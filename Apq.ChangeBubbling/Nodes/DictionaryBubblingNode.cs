using System.Collections;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Collections;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Nodes;

/// <summary>
/// 基于 <see cref="IDictionary"/> 的更改事件冒泡节点。
/// 使用 <see cref="ObservableCollectionAdapter"/> 代理底层集合，
/// 将 Add/Remove/Indexer 等操作映射为 <see cref="BubblingChange"/> 并向上冒泡。
/// 适用于任意 <typeparamref name="TValue"/> 类型（无需实现 IChangeNode）。
/// <para>注意：此类非线程安全。如需线程安全版本，请使用 <see cref="Concurrent.ConcurrentDictionaryBubblingNode{TKey, TValue}"/>。</para>
/// </summary>
public class DictionaryBubblingNode<TKey, TValue> : ChangeNodeBase
    where TKey : notnull
{
    /// <summary>底层数据字典。</summary>
    protected readonly IDictionary<TKey, TValue> Data;
    /// <summary>集合变更适配器。</summary>
    protected readonly ObservableCollectionAdapter Adapter;

    // 快照缓存，避免每次访问都创建新集合
    private volatile IReadOnlyCollection<TKey>? _keysSnapshot;
    private volatile IReadOnlyCollection<KeyValuePair<TKey, TValue>>? _itemsSnapshot;

    /// <summary>
    /// 创建节点。
    /// </summary>
    /// <param name="name">节点名称（用于冒泡路径）。</param>
    public DictionaryBubblingNode(string name)
    {
        Name = name;
        Data = new Dictionary<TKey, TValue>();
        Adapter = new ObservableCollectionAdapter((IDictionary)Data, "Data");
        Adapter.NodeChanged += HandleAdapterNodeChanged;
    }

    /// <summary>当前键集合的快照（缓存，集合变更时自动失效）。</summary>
    public IReadOnlyCollection<TKey> Keys
    {
        get
        {
            var snapshot = _keysSnapshot;
            if (snapshot is not null) return snapshot;

            snapshot = Data.Keys.ToArray();
            _keysSnapshot = snapshot;
            return snapshot;
        }
    }

    /// <summary>当前项集合的快照（缓存，集合变更时自动失效）。</summary>
    public IReadOnlyCollection<KeyValuePair<TKey, TValue>> Items
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

    /// <summary>设置或新增键值（通过代理触发集合更改）。</summary>
    public void Put(TKey key, TValue value)
    {
        InvalidateSnapshots();
        // Adapter.Proxied 可能是 IDictionary<TKey,TValue> 的代理，优先走泛型
        if (Adapter.Proxied is IDictionary<TKey, TValue> gdict)
        {
            gdict[key] = value;
        }
        else if (Adapter.Proxied is IDictionary ndict)
        {
            ndict[key!] = value!;
        }
    }

    /// <summary>尝试读取。</summary>
    public bool TryGet(TKey key, out TValue? value) => Data.TryGetValue(key, out value);

    /// <summary>移除键（通过代理触发集合更改）。</summary>
    public bool Remove(TKey key)
    {
        if (!Data.ContainsKey(key)) return false;
        InvalidateSnapshots();
        if (Adapter.Proxied is IDictionary<TKey, TValue> gdict)
        {
            return gdict.Remove(key);
        }
        else if (Adapter.Proxied is IDictionary ndict)
        {
            ndict.Remove(key!);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 静默批量填充（不经过代理，不产生集合事件），适合初始化/克隆。
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
    /// 处理适配器的节点变更事件（避免 Lambda 闭包分配）。
    /// </summary>
    private void HandleAdapterNodeChanged(object? sender, BubblingChange e)
    {
        InvalidateSnapshots();
        RaiseNodeChangedWithKeyPath(e);
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

        var keyStr = originalChange.Key.ToString() ?? "Unknown";

        // 创建新的变更事件，将键作为属性名
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


