using System.Collections;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Collections;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Nodes;

/// <summary>
/// 基于 <see cref="IList"/> 的更改事件冒泡节点。
/// 使用 <see cref="ObservableCollectionAdapter"/> 代理底层集合，
/// 将 Add/Insert/Remove/Indexer 等操作映射为 <see cref="BubblingChange"/> 并向上冒泡。
/// 适用于任意 <typeparamref name="T"/> 类型（无需实现 IChangeNode）。
/// <para>注意：此类非线程安全。如需线程安全版本，请使用 <see cref="Concurrent.ConcurrentBagBubblingNode{T}"/>。</para>
/// </summary>
public class ListBubblingNode<T> : ChangeNodeBase
{
    /// <summary>底层数据列表。</summary>
    protected readonly IList<T> Data;
    /// <summary>集合变更适配器。</summary>
    protected readonly ObservableCollectionAdapter Adapter;

    // 快照缓存，避免每次访问 Items 都创建新集合
    private volatile IReadOnlyList<T>? _itemsSnapshot;

    /// <summary>
    /// 创建节点。
    /// </summary>
    /// <param name="name">节点名称（用于冒泡路径）。</param>
    public ListBubblingNode(string name)
    {
        Name = name;
        Data = new List<T>();
        Adapter = new ObservableCollectionAdapter((IList)Data, "Items");
        Adapter.NodeChanged += HandleAdapterNodeChanged;
    }

    /// <summary>当前项集合的快照（缓存，集合变更时自动失效）。</summary>
    public IReadOnlyList<T> Items
    {
        get
        {
            var snapshot = _itemsSnapshot;
            if (snapshot is not null) return snapshot;

            snapshot = Data.ToArray();
            _itemsSnapshot = snapshot;
            return snapshot;
        }
    }

    /// <summary>当前元素数量（直接访问底层集合，无需快照）。</summary>
    public int Count => Data.Count;

    /// <summary>在末尾添加（通过代理触发集合更改）。</summary>
    public void Add(T value)
    {
        InvalidateItemsSnapshot();
        if (Adapter.Proxied is IList<T> glist)
        {
            glist.Add(value!);
        }
        else if (Adapter.Proxied is IList nlist)
        {
            nlist.Add(value!);
        }
    }

    /// <summary>在指定位置插入（通过代理触发集合更改）。</summary>
    public void Insert(int index, T value)
    {
        InvalidateItemsSnapshot();
        if (Adapter.Proxied is IList<T> glist)
        {
            glist.Insert(index, value!);
        }
        else if (Adapter.Proxied is IList nlist)
        {
            nlist.Insert(index, value!);
        }
    }

    /// <summary>按索引移除（通过代理触发集合更改）。</summary>
    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= Data.Count) return false;
        InvalidateItemsSnapshot();
        if (Adapter.Proxied is IList<T> glist)
        {
            glist.RemoveAt(index);
        }
        else if (Adapter.Proxied is IList nlist)
        {
            nlist.RemoveAt(index);
        }
        return true;
    }

    /// <summary>按值移除（通过代理触发集合更改）。</summary>
    public bool Remove(T value)
    {
        var idx = Data.IndexOf(value);
        if (idx < 0) return false;
        return RemoveAt(idx);
    }

    /// <summary>
    /// 静默批量填充（不经过代理，不产生集合事件），适合初始化/克隆。
    /// </summary>
    public void PopulateSilently(IEnumerable<T> values)
    {
        foreach (var v in values)
        {
            Data.Add(v);
        }
        InvalidateItemsSnapshot();
    }

    /// <summary>
    /// 使快照缓存失效。
    /// </summary>
    private void InvalidateItemsSnapshot() => _itemsSnapshot = null;

    /// <summary>
    /// 处理适配器的节点变更事件（避免 Lambda 闭包分配）。
    /// </summary>
    private void HandleAdapterNodeChanged(object? sender, BubblingChange e)
    {
        RaiseNodeChangedWithIndexPath(e);
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

        var index = originalChange.Index.Value;
        var indexStr = PathSegmentCache.GetIndexString(index);

        // 创建新的变更事件，将索引作为属性名
        var indexPathChange = new BubblingChange
        {
            PropertyName = indexStr,
            Kind = originalChange.Kind,
            PathSegments = PathSegmentCache.GetIndexSegment(index),
            OldValue = originalChange.OldValue,
            NewValue = originalChange.NewValue,
            Index = originalChange.Index,
            Key = originalChange.Key
        };

        RaiseNodeChanged(indexPathChange);
    }
}


