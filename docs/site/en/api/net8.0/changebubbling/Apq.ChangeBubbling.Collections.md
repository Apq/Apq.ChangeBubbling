#### [Apq\.ChangeBubbling](index.md 'index')

## Apq\.ChangeBubbling\.Collections Namespace

| Classes | |
| :--- | :--- |
| [CollectionAdapters](Apq.ChangeBubbling.Collections.CollectionAdapters.md 'Apq\.ChangeBubbling\.Collections\.CollectionAdapters') | 工厂方法集合：基于 Castle 代理为集合创建可观察适配器。 |
| [ObservableCollectionAdapter](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter') | 基于 Castle DynamicProxy 的通用集合适配器：为 IList/ICollection/IDictionary 等集合生成代理， 截获修改方法并触发 BubblingChange。 注意：该适配器聚焦"通知"，不强行统一所有集合 API，仅拦截常见变更入口。 |

| Interfaces | |
| :--- | :--- |
| [IMultiValueStore](Apq.ChangeBubbling.Collections.IMultiValueStore.md 'Apq\.ChangeBubbling\.Collections\.IMultiValueStore') | 统一的多值存储抽象，屏蔽 List/Dictionary 等差异，同时提供冒泡事件。 Castle 动态代理的通用适配器将实现该接口。 |
