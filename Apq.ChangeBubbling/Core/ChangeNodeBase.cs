using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Core;

/// <summary>
/// 提供父子管理与冒泡转译的基类。派生类可借助 Fody 自动织入属性变更。
/// 使用弱事件订阅避免内存泄漏。线程安全。
/// </summary>
public abstract class ChangeNodeBase : IChangeNode
{
    // 使用 ConcurrentDictionary 同时作为子节点集合和订阅管理，O(1) 添加/删除
    private readonly ConcurrentDictionary<IChangeNode, WeakEventSubscription> _childSubscriptions = new(ReferenceEqualityComparer.Instance);

    // 用于保持子节点顺序的列表（如果需要顺序访问）
    private readonly object _childrenLock = new();
    private volatile IChangeNode[]? _childrenSnapshot;

    // 缓存当前节点的深度，避免每次冒泡都遍历计算
    private volatile int _cachedDepth = -1;

    // 批量操作支持
    private readonly object _batchLock = new();
    private int _batchDepth;
    private volatile bool _isInBatch; // volatile 标志位，支持无锁读取
    private List<BubblingChange>? _batchedChanges;

    // ThreadStatic 缓存的 Stack，避免 InvalidateDepthCache 每次分配
    [ThreadStatic]
    private static Stack<ChangeNodeBase>? t_invalidateStack;

    /// <inheritdoc />
    public string Name { get; protected init; } = string.Empty;
    /// <inheritdoc />
    public IChangeNode? Parent { get; private set; }
    /// <inheritdoc />
    public IReadOnlyList<IChangeNode> Children
    {
        get
        {
            // 返回快照，避免并发修改问题
            var snapshot = _childrenSnapshot;
            if (snapshot is not null) return snapshot;

            lock (_childrenLock)
            {
                snapshot = _childrenSnapshot;
                if (snapshot is not null) return snapshot;

                snapshot = _childSubscriptions.Keys.ToArray();
                _childrenSnapshot = snapshot;
                return snapshot;
            }
        }
    }

    /// <summary>
    /// 由 Fody 自动织入：属性变更事件。
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    /// <inheritdoc />
    public event EventHandler<BubblingChange>? NodeChanged;

    /// <summary>
    /// 触发节点变更事件，并通过弱消息发布。
    /// </summary>
    /// <param name="change">变更上下文。</param>
    protected void RaiseNodeChanged(BubblingChange change)
    {
        NodeChanged?.Invoke(this, change);
        // 同步发布消息（弱引用）
        Messaging.ChangeMessenger.Publish(change);
    }

    /// <inheritdoc />
    public virtual void AttachChild(IChangeNode child)
    {
        ArgumentNullException.ThrowIfNull(child);
        if (ReferenceEquals(child, this)) throw new InvalidOperationException("不能将节点自身作为子节点");

        // 使用 ConcurrentDictionary 进行 O(1) 查重和添加
        var subscription = new WeakEventSubscription(child, this);
        if (!_childSubscriptions.TryAdd(child, subscription))
        {
            subscription.Dispose();
            return; // 已存在，不重复添加
        }

        // 检测父子回环
        for (IChangeNode? p = this; p is not null; p = p.Parent)
        {
            if (ReferenceEquals(p, child))
            {
                // 回滚：移除刚添加的订阅
                if (_childSubscriptions.TryRemove(child, out var sub))
                    sub.Dispose();
                throw new InvalidOperationException("检测到父子回环");
            }
        }

        if (child is ChangeNodeBase baseChild)
        {
            baseChild.Parent = this;
            baseChild.InvalidateDepthCache();
        }

        // 使快照失效
        _childrenSnapshot = null;
    }

    /// <inheritdoc />
    public virtual void DetachChild(IChangeNode child)
    {
        if (child is null) return;

        // 使用 ConcurrentDictionary 的原子移除操作
        if (!_childSubscriptions.TryRemove(child, out var subscription)) return;

        // 清理弱事件订阅
        subscription.Dispose();

        if (child is ChangeNodeBase baseChild)
        {
            baseChild.Parent = null;
            baseChild.InvalidateDepthCache();
        }

        // 使快照失效
        _childrenSnapshot = null;
    }

    /// <summary>
    /// 内部方法：处理子节点属性变更（由 WeakEventSubscription 调用）。
    /// </summary>
    internal void HandleChildPropertyChangedInternal(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not IChangeNode child) return;

        var path = BuildPathWithChild(this, child.Name);
        var change = new BubblingChange
        {
            PropertyName = e.PropertyName ?? string.Empty,
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = path
        };
        RaiseNodeChanged(change);
    }

    /// <summary>
    /// 内部方法：处理子节点变更（由 WeakEventSubscription 调用）。
    /// </summary>
    internal void HandleChildNodeChangedInternal(object? sender, BubblingChange e)
    {
        var path = BuildPathWithSegments(this, e.PathSegments);
        var forwarded = new BubblingChange
        {
            PropertyName = e.PropertyName,
            Kind = e.Kind,
            PathSegments = path,
            OldValue = e.OldValue,
            NewValue = e.NewValue,
            Index = e.Index,
            Key = e.Key
        };
        RaiseNodeChanged(forwarded);
    }

    /// <summary>
    /// 使深度缓存失效（使用迭代避免深层树结构的栈溢出）。
    /// 使用 ThreadStatic 缓存的 Stack 避免每次调用都分配新对象。
    /// </summary>
    private void InvalidateDepthCache()
    {
        _cachedDepth = -1;

        // 使用 ThreadStatic 缓存的栈，避免每次分配
        var stack = t_invalidateStack ??= new Stack<ChangeNodeBase>();

        // 将当前节点的子节点入栈
        foreach (var child in _childSubscriptions.Keys)
        {
            if (child is ChangeNodeBase baseChild)
                stack.Push(baseChild);
        }

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            node._cachedDepth = -1;

            foreach (var child in node._childSubscriptions.Keys)
            {
                if (child is ChangeNodeBase baseChild)
                    stack.Push(baseChild);
            }
        }

        // 注意：不需要 Clear()，因为循环结束时 stack 已经为空
    }

    /// <summary>
    /// 获取节点深度（从根到当前节点的路径长度）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetDepth()
    {
        if (_cachedDepth >= 0) return _cachedDepth;

        var depth = 0;
        for (var p = Parent; p is not null; p = p.Parent)
        {
            if (!string.IsNullOrEmpty(p.Name)) depth++;
        }
        if (!string.IsNullOrEmpty(Name)) depth++;

        _cachedDepth = depth;
        return depth;
    }

    /// <summary>
    /// 构建路径并追加子节点名称。
    /// 对于短路径（深度 <= 16）直接分配数组，避免 ArrayPool 开销。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string[] BuildPathWithChild(ChangeNodeBase node, string childName)
    {
        var depth = node.GetDepth();

        // 快速路径：深度为 0 时只有子节点名称
        if (depth == 0)
        {
            return PathSegmentCache.GetSingle(childName);
        }

        var totalLength = depth + 1; // +1 for child name

        // 短路径优化：深度 <= 16 时直接分配，避免 ArrayPool 开销
        // ArrayPool 的租借/归还开销在短数组时可能超过直接分配
        if (totalLength <= 16)
        {
            var result = new string[totalLength];
            var index = totalLength - 1;
            result[index--] = childName;
            FillPathFromNode(node, result, ref index);
            return result;
        }

        // 长路径：使用 ArrayPool 租借数组，减少 GC 压力
        var rented = ArrayPool<string>.Shared.Rent(totalLength);
        try
        {
            var index = totalLength - 1;

            // 填充子节点名称
            rented[index--] = childName;

            // 从当前节点向上填充
            FillPathFromNode(node, rented, ref index);

            // 创建精确大小的结果数组（因为租借的数组可能更大）
            var result = new string[totalLength];
            Array.Copy(rented, result, totalLength);
            return result;
        }
        finally
        {
            ArrayPool<string>.Shared.Return(rented, clearArray: true);
        }
    }

    /// <summary>
    /// 构建路径并追加已有路径段。
    /// 对于短路径（总长度 <= 16）直接分配数组，避免 ArrayPool 开销。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string[] BuildPathWithSegments(ChangeNodeBase node, IReadOnlyList<string> existingSegments)
    {
        var depth = node.GetDepth();
        var existingCount = existingSegments.Count;

        // 快速路径：深度为 0 时直接返回已有路径段（如果已经是数组）
        if (depth == 0)
        {
            if (existingSegments is string[] arr)
                return arr;

            // 需要转换为数组
            var copy = new string[existingCount];
            for (var i = 0; i < existingCount; i++)
                copy[i] = existingSegments[i];
            return copy;
        }

        var totalLength = depth + existingCount;

        // 短路径优化：总长度 <= 16 时直接分配
        if (totalLength <= 16)
        {
            var result = new string[totalLength];
            // 复制已有路径段
            for (var i = 0; i < existingCount; i++)
                result[depth + i] = existingSegments[i];
            var index = depth - 1;
            FillPathFromNode(node, result, ref index);
            return result;
        }

        // 长路径：使用 ArrayPool 租借数组，减少 GC 压力
        var rented = ArrayPool<string>.Shared.Rent(totalLength);
        try
        {
            // 复制已有路径段到末尾
            for (var i = 0; i < existingCount; i++)
                rented[depth + i] = existingSegments[i];

            // 从当前节点向上填充
            var index = depth - 1;
            FillPathFromNode(node, rented, ref index);

            // 创建精确大小的结果数组
            var result = new string[totalLength];
            Array.Copy(rented, result, totalLength);
            return result;
        }
        finally
        {
            ArrayPool<string>.Shared.Return(rented, clearArray: true);
        }
    }

    /// <summary>
    /// 从节点向上填充路径数组。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void FillPathFromNode(ChangeNodeBase node, string[] result, ref int index)
    {
        // 填充当前节点名称
        if (!string.IsNullOrEmpty(node.Name))
        {
            result[index--] = node.Name;
        }

        // 向上遍历父节点
        for (var p = node.Parent; p is not null; p = p.Parent)
        {
            if (!string.IsNullOrEmpty(p.Name))
            {
                result[index--] = p.Name;
            }
        }
    }

    #region 批量操作支持

    /// <summary>
    /// 开始批量操作。在批量操作期间，变更事件会被收集而不是立即触发。
    /// 支持嵌套调用，只有最外层的 EndBatch 才会触发事件。
    /// </summary>
    public void BeginBatch()
    {
        lock (_batchLock)
        {
            _batchDepth++;
            _isInBatch = true;
            _batchedChanges ??= new List<BubblingChange>();
        }
    }

    /// <summary>
    /// 结束批量操作。如果这是最外层的批量操作，则触发所有收集的变更事件。
    /// </summary>
    /// <param name="raiseAggregated">是否触发聚合事件（默认 true）。</param>
    public void EndBatch(bool raiseAggregated = true)
    {
        List<BubblingChange>? changesToRaise = null;

        lock (_batchLock)
        {
            if (_batchDepth <= 0) return;

            _batchDepth--;

            if (_batchDepth == 0)
            {
                _isInBatch = false;
                if (_batchedChanges is not null)
                {
                    if (raiseAggregated && _batchedChanges.Count > 0)
                    {
                        changesToRaise = _batchedChanges;
                    }
                    _batchedChanges = null;
                }
            }
        }

        // 在锁外触发事件，避免死锁
        if (changesToRaise is not null)
        {
            foreach (var change in changesToRaise)
            {
                NodeChanged?.Invoke(this, change);
                Messaging.ChangeMessenger.Publish(change);
            }
        }
    }

    /// <summary>
    /// 获取当前是否处于批量操作中（无锁快速读取）。
    /// </summary>
    public bool IsInBatch => _isInBatch;

    /// <summary>
    /// 内部方法：在批量模式下收集变更，否则立即触发。
    /// 派生类应使用此方法替代直接调用 RaiseNodeChanged。
    /// </summary>
    protected void RaiseNodeChangedBatched(BubblingChange change)
    {
        // 快速路径：无锁检查，绝大多数情况下不在批量模式
        if (!_isInBatch)
        {
            RaiseNodeChanged(change);
            return;
        }

        // 慢速路径：需要加锁确认并添加到批量列表
        lock (_batchLock)
        {
            if (_batchDepth > 0 && _batchedChanges is not null)
            {
                _batchedChanges.Add(change);
                return;
            }
        }

        // 竞态条件：在无锁检查和加锁之间批量模式结束了
        RaiseNodeChanged(change);
    }

    #endregion

    #region 事件合并支持

    // 事件合并相关字段
    private readonly object _coalesceLock = new();
    private Dictionary<string, BubblingChange>? _coalescedChanges;
    private volatile bool _isCoalescing; // volatile 标志位，支持无锁读取

    /// <summary>
    /// 开始事件合并模式。在此模式下，相同属性名的变更事件会被合并，
    /// 只保留最新的值（OldValue 保留第一次的，NewValue 使用最后一次的）。
    /// 适用于高频属性更新场景，减少下游处理压力。
    /// </summary>
    public void BeginCoalesce()
    {
        lock (_coalesceLock)
        {
            _isCoalescing = true;
            _coalescedChanges ??= new Dictionary<string, BubblingChange>(StringComparer.Ordinal);
        }
    }

    /// <summary>
    /// 结束事件合并模式，触发所有合并后的变更事件。
    /// </summary>
    public void EndCoalesce()
    {
        Dictionary<string, BubblingChange>? changesToRaise = null;

        lock (_coalesceLock)
        {
            if (!_isCoalescing) return;

            _isCoalescing = false;
            if (_coalescedChanges is not null && _coalescedChanges.Count > 0)
            {
                changesToRaise = _coalescedChanges;
                _coalescedChanges = null;
            }
        }

        // 在锁外触发事件，避免死锁
        if (changesToRaise is not null)
        {
            foreach (var change in changesToRaise.Values)
            {
                NodeChanged?.Invoke(this, change);
                Messaging.ChangeMessenger.Publish(change);
            }
        }
    }

    /// <summary>
    /// 获取当前是否处于事件合并模式（无锁快速读取）。
    /// </summary>
    public bool IsCoalescing => _isCoalescing;

    /// <summary>
    /// 触发节点变更事件，支持事件合并模式。
    /// 在合并模式下，相同属性名的事件会被合并。
    /// </summary>
    /// <param name="change">变更上下文。</param>
    protected void RaiseNodeChangedCoalesced(BubblingChange change)
    {
        // 快速路径：无锁检查，绝大多数情况下不在合并模式
        if (!_isCoalescing)
        {
            RaiseNodeChanged(change);
            return;
        }

        // 慢速路径：需要加锁确认并添加到合并字典
        lock (_coalesceLock)
        {
            if (_isCoalescing && _coalescedChanges is not null)
            {
                // 合并逻辑：保留第一次的 OldValue，使用最新的 NewValue
                if (_coalescedChanges.TryGetValue(change.PropertyName, out var existing))
                {
                    // 使用 with 表达式，减少复制开销
                    _coalescedChanges[change.PropertyName] = change with { OldValue = existing.OldValue };
                }
                else
                {
                    _coalescedChanges[change.PropertyName] = change;
                }
                return;
            }
        }

        // 竞态条件：在无锁检查和加锁之间合并模式结束了
        RaiseNodeChanged(change);
    }

    #endregion
}
