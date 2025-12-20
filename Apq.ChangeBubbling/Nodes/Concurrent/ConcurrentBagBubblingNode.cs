using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Collections;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Nodes.Concurrent;

/// <summary>
/// 基于线程安全列表的更改事件冒泡节点。
/// 将 Add/Insert/Remove/Indexer 等操作映射为 <see cref="BubblingChange"/> 并向上冒泡。
/// 适用于任意 <typeparamref name="T"/> 类型（无需实现 IChangeNode）。
/// 所有操作都是线程安全的。
/// </summary>
public class ConcurrentBagBubblingNode<T> : ChangeNodeBase
{
    /// <summary>底层数据列表，使用锁保护。</summary>
    private readonly List<T> _list;
    private readonly object _sync = new();

    // 快照缓存，避免每次访问都创建新集合
    private volatile IReadOnlyList<T>? _itemsSnapshot;

    /// <summary>
    /// 创建线程安全节点。
    /// </summary>
    /// <param name="name">节点名称（用于冒泡路径）。</param>
    public ConcurrentBagBubblingNode(string name)
    {
        Name = name;
        _list = new List<T>();
    }

    /// <summary>当前项集合的快照（缓存，集合变更时自动失效）。</summary>
    public IReadOnlyList<T> Items
    {
        get
        {
            var snapshot = _itemsSnapshot;
            if (snapshot is not null) return snapshot;

            lock (_sync)
            {
                snapshot = _itemsSnapshot;
                if (snapshot is not null) return snapshot;

                snapshot = _list.ToArray();
                _itemsSnapshot = snapshot;
                return snapshot;
            }
        }
    }

    /// <summary>当前元素数量。</summary>
    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _list.Count;
            }
        }
    }

    /// <summary>在末尾添加。</summary>
    public void Add(T value)
    {
        BubblingChange change;
        lock (_sync)
        {
            _itemsSnapshot = null; // 失效快照
            var index = _list.Count;
            _list.Add(value!);

            change = new BubblingChange
            {
                PropertyName = "Items",
                Kind = NodeChangeKind.CollectionAdd,
                PathSegments = PathSegmentCache.GetSingle("Items"),
                OldValue = null,
                NewValue = value,
                Index = index
            };
        }
        // 锁外触发事件，避免阻塞其他线程
        RaiseNodeChangedWithIndexPath(change);
    }

    /// <summary>在指定位置插入。</summary>
    public void Insert(int index, T value)
    {
        BubblingChange change;
        lock (_sync)
        {
            _itemsSnapshot = null; // 失效快照
            if (index < 0 || index > _list.Count) index = _list.Count;
            _list.Insert(index, value!);

            change = new BubblingChange
            {
                PropertyName = "Items",
                Kind = NodeChangeKind.CollectionAdd,
                PathSegments = PathSegmentCache.GetSingle("Items"),
                OldValue = null,
                NewValue = value,
                Index = index
            };
        }
        // 锁外触发事件
        RaiseNodeChangedWithIndexPath(change);
    }

    /// <summary>按索引移除。</summary>
    public bool RemoveAt(int index)
    {
        BubblingChange change;
        lock (_sync)
        {
            if (index < 0 || index >= _list.Count) return false;
            _itemsSnapshot = null; // 失效快照
            var value = _list[index]!;
            _list.RemoveAt(index);

            change = new BubblingChange
            {
                PropertyName = "Items",
                Kind = NodeChangeKind.CollectionRemove,
                PathSegments = PathSegmentCache.GetSingle("Items"),
                OldValue = value,
                NewValue = null,
                Index = index
            };
        }
        // 锁外触发事件
        RaiseNodeChangedWithIndexPath(change);
        return true;
    }

    /// <summary>按值移除。</summary>
    public bool Remove(T value)
    {
        BubblingChange change;
        lock (_sync)
        {
            var idx = _list.IndexOf(value!);
            if (idx < 0) return false;
            _itemsSnapshot = null; // 失效快照
            _list.RemoveAt(idx);

            change = new BubblingChange
            {
                PropertyName = "Items",
                Kind = NodeChangeKind.CollectionRemove,
                PathSegments = PathSegmentCache.GetSingle("Items"),
                OldValue = value,
                NewValue = null,
                Index = idx
            };
        }
        // 锁外触发事件
        RaiseNodeChangedWithIndexPath(change);
        return true;
    }

    /// <summary>清空所有元素。</summary>
    public void Clear()
    {
        bool shouldRaise;
        lock (_sync)
        {
            shouldRaise = _list.Count > 0;
            if (shouldRaise)
            {
                _itemsSnapshot = null; // 失效快照
                _list.Clear();
            }
        }

        if (shouldRaise)
        {
            var change = new BubblingChange
            {
                PropertyName = "Items",
                Kind = NodeChangeKind.CollectionReset,
                PathSegments = PathSegmentCache.GetSingle("Items"),
                OldValue = null,
                NewValue = null,
                Index = null
            };
            RaiseNodeChanged(change);
        }
    }

    /// <summary>
    /// 静默批量填充（不产生集合事件），适合初始化/克隆。
    /// </summary>
    public void PopulateSilently(IEnumerable<T> values)
    {
        lock (_sync)
        {
            _itemsSnapshot = null; // 失效快照
            _list.AddRange(values);
        }
    }

    /// <summary>
    /// 将集合变更事件重新包装，使索引直接作为属性名，跳过中间层级。
    /// </summary>
    private void RaiseNodeChangedWithIndexPath(BubblingChange originalChange)
    {
        if (originalChange.Index is null)
        {
            // 没有索引信息，直接冒泡原事件
            RaiseNodeChanged(originalChange);
            return;
        }

        // 创建新的变更事件，将索引作为属性名
        var idx = originalChange.Index.Value;
        var indexPathChange = new BubblingChange
        {
            PropertyName = PathSegmentCache.GetIndexString(idx),
            Kind = originalChange.Kind,
            PathSegments = PathSegmentCache.GetIndexSegment(idx),
            OldValue = originalChange.OldValue,
            NewValue = originalChange.NewValue,
            Index = originalChange.Index,
            Key = originalChange.Key
        };

        RaiseNodeChanged(indexPathChange);
    }
}
