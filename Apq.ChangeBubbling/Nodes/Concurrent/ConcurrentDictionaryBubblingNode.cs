using System.Collections.Concurrent;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Nodes.Concurrent;

/// <summary>
/// 基于 <see cref="ConcurrentDictionary"/> 的线程安全更改事件冒泡节点。
/// 直接使用 ConcurrentDictionary 的原子操作，手动触发变更事件。
/// 适用于任意 <typeparamref name="TValue"/> 类型（无需实现 IChangeNode）。
/// 所有操作都是线程安全的。
/// </summary>
public class ConcurrentDictionaryBubblingNode<TKey, TValue> : ChangeNodeBase
    where TKey : notnull
{
    /// <summary>底层线程安全数据字典。</summary>
    protected readonly ConcurrentDictionary<TKey, TValue> Data;

    // 快照缓存，避免每次访问都创建新集合
    private volatile IReadOnlyCollection<TKey>? _keysSnapshot;
    private volatile IReadOnlyCollection<KeyValuePair<TKey, TValue>>? _itemsSnapshot;
    private readonly object _snapshotLock = new();

    /// <summary>
    /// 创建线程安全节点。
    /// </summary>
    /// <param name="name">节点名称（用于冒泡路径）。</param>
    public ConcurrentDictionaryBubblingNode(string name)
    {
        Name = name;
        Data = new ConcurrentDictionary<TKey, TValue>();
    }

    /// <summary>当前键集合的快照（缓存，集合变更时自动失效，线程安全）。</summary>
    public IReadOnlyCollection<TKey> Keys
    {
        get
        {
            var snapshot = _keysSnapshot;
            if (snapshot is not null) return snapshot;

            lock (_snapshotLock)
            {
                snapshot = _keysSnapshot;
                if (snapshot is not null) return snapshot;

                snapshot = Data.Keys.ToArray();
                _keysSnapshot = snapshot;
                return snapshot;
            }
        }
    }

    /// <summary>当前项集合的快照（缓存，集合变更时自动失效，线程安全）。</summary>
    public IReadOnlyCollection<KeyValuePair<TKey, TValue>> Items
    {
        get
        {
            var snapshot = _itemsSnapshot;
            if (snapshot is not null) return snapshot;

            lock (_snapshotLock)
            {
                snapshot = _itemsSnapshot;
                if (snapshot is not null) return snapshot;

                snapshot = Data.ToArray();
                _itemsSnapshot = snapshot;
                return snapshot;
            }
        }
    }

    /// <summary>当前元素数量（无锁快速路径）。</summary>
    public int Count => Data.Count;

    /// <summary>设置或新增键值（使用原子操作，正确区分 Add 和 Update）。</summary>
    public void Put(TKey key, TValue value)
    {
        TValue? oldValue = default;
        var isAdd = false;

        Data.AddOrUpdate(
            key,
            _ =>
            {
                isAdd = true;
                return value;
            },
            (_, existing) =>
            {
                oldValue = existing;
                return value;
            });

        InvalidateSnapshots();

        // 根据操作类型触发正确的变更事件
        var change = new BubblingChange
        {
            PropertyName = "Data",
            Kind = isAdd ? NodeChangeKind.CollectionAdd : NodeChangeKind.CollectionReplace,
            PathSegments = PathSegmentCache.GetSingle("Data"),
            OldValue = isAdd ? null : oldValue,
            NewValue = value,
            Index = null,
            Key = key
        };
        RaiseNodeChangedWithKeyPath(change);
    }

    /// <summary>尝试读取。</summary>
    public bool TryGet(TKey key, out TValue? value) => Data.TryGetValue(key, out value);

    /// <summary>移除键（通过代理触发集合更改）。使用原子操作避免竞态条件。</summary>
    public bool Remove(TKey key)
    {
        // 使用 TryRemove 原子操作，避免 ContainsKey 和 Remove 之间的竞态窗口
        if (!Data.TryRemove(key, out var removedValue))
            return false;

        InvalidateSnapshots();

        // 手动触发变更事件（因为绕过了代理）
        var change = new BubblingChange
        {
            PropertyName = "Data",
            Kind = NodeChangeKind.CollectionRemove,
            PathSegments = PathSegmentCache.GetSingle("Data"),
            OldValue = removedValue,
            NewValue = null,
            Index = null,
            Key = key
        };
        RaiseNodeChangedWithKeyPath(change);
        return true;
    }

    /// <summary>
    /// 静默批量填充（不产生集合事件），适合初始化/克隆。
    /// </summary>
    public void PopulateSilently(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        foreach (var kv in items)
        {
            Data[kv.Key] = kv.Value;
        }
        InvalidateSnapshots();
    }

    /// <summary>
    /// 使快照缓存失效。
    /// </summary>
    private void InvalidateSnapshots()
    {
        _keysSnapshot = null;
        _itemsSnapshot = null;
    }

    /// <summary>
    /// 将集合变更事件重新包装，使键直接作为属性名，跳过中间层级。
    /// </summary>
    private void RaiseNodeChangedWithKeyPath(BubblingChange originalChange)
    {
        if (originalChange.Key is null)
        {
            // 没有键信息，直接冒泡原事件
            RaiseNodeChanged(originalChange);
            return;
        }

        // 创建新的变更事件，将键作为属性名
        var keyStr = originalChange.Key.ToString() ?? "Unknown";
        var keyPathChange = new BubblingChange
        {
            PropertyName = keyStr,
            Kind = originalChange.Kind,
            PathSegments = PathSegmentCache.GetSingle(keyStr),
            OldValue = originalChange.OldValue,
            NewValue = originalChange.NewValue,
            Index = originalChange.Index,
            Key = originalChange.Key
        };

        RaiseNodeChanged(keyPathChange);
    }
}
