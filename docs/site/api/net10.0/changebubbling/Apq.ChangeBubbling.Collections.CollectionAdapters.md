#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Collections](Apq.ChangeBubbling.Collections.md 'Apq\.ChangeBubbling\.Collections')

## CollectionAdapters Class

工厂方法集合：基于 Castle 代理为集合创建可观察适配器。

```csharp
public static class CollectionAdapters
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; CollectionAdapters

| Methods | |
| :--- | :--- |
| [Create\(object, string\)](Apq.ChangeBubbling.Collections.CollectionAdapters.Create(object,string).md 'Apq\.ChangeBubbling\.Collections\.CollectionAdapters\.Create\(object, string\)') | 为任意集合（如 IList、IDictionary、自定义集合）创建可观察适配器。 返回的适配器提供 [Proxied](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.Proxied.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter\.Proxied') 供替代原集合引用。 |
