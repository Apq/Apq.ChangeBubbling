#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Collections](Apq.ChangeBubbling.Collections.md 'Apq\.ChangeBubbling\.Collections')

## ObservableCollectionAdapter Class

基于 Castle DynamicProxy 的通用集合适配器：为 IList/ICollection/IDictionary 等集合生成代理，
截获修改方法并触发 BubblingChange。
注意：该适配器聚焦"通知"，不强行统一所有集合 API，仅拦截常见变更入口。

```csharp
public sealed class ObservableCollectionAdapter : Apq.ChangeBubbling.Collections.IMultiValueStore, Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier, System.Collections.IEnumerable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; ObservableCollectionAdapter

Implements [IMultiValueStore](Apq.ChangeBubbling.Collections.IMultiValueStore.md 'Apq\.ChangeBubbling\.Collections\.IMultiValueStore'), [IBubblingChangeNotifier](Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier.md 'Apq\.ChangeBubbling\.Abstractions\.IBubblingChangeNotifier'), [System\.Collections\.IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable 'System\.Collections\.IEnumerable')

| Properties | |
| :--- | :--- |
| [Proxied](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.Proxied.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter\.Proxied') | 代理对象，供上层替代原集合引用进行使用。 |

| Methods | |
| :--- | :--- |
| [FindBestInterface\(Type\)](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.FindBestInterface(System.Type).md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter\.FindBestInterface\(System\.Type\)') | 查找最佳代理接口（静态方法，用于缓存）。 |
