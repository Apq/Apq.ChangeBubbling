#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Collections](Apq.ChangeBubbling.Collections.md 'Apq\.ChangeBubbling\.Collections').[CollectionAdapters](Apq.ChangeBubbling.Collections.CollectionAdapters.md 'Apq\.ChangeBubbling\.Collections\.CollectionAdapters')

## CollectionAdapters\.Create\(object, string\) Method

为任意集合（如 IList、IDictionary、自定义集合）创建可观察适配器。
返回的适配器提供 [Proxied](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.Proxied.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter\.Proxied') 供替代原集合引用。

```csharp
public static Apq.ChangeBubbling.Collections.ObservableCollectionAdapter Create(object collection, string name);
```
#### Parameters

<a name='Apq.ChangeBubbling.Collections.CollectionAdapters.Create(object,string).collection'></a>

`collection` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

<a name='Apq.ChangeBubbling.Collections.CollectionAdapters.Create(object,string).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[ObservableCollectionAdapter](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter')