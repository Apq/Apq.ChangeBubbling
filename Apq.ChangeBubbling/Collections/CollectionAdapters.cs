using System.Collections;

namespace Apq.ChangeBubbling.Collections;

/// <summary>
/// 工厂方法集合：基于 Castle 代理为集合创建可观察适配器。
/// </summary>
public static class CollectionAdapters
{
    /// <summary>
    /// 为任意集合（如 IList、IDictionary、自定义集合）创建可观察适配器。
    /// 返回的适配器提供 <see cref="ObservableCollectionAdapter.Proxied"/> 供替代原集合引用。
    /// </summary>
    public static ObservableCollectionAdapter Create(object collection, string name)
    {
        return new ObservableCollectionAdapter(collection, name);
    }
}


