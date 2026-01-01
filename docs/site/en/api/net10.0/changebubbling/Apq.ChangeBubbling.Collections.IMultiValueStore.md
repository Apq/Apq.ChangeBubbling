#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Collections](Apq.ChangeBubbling.Collections.md 'Apq\.ChangeBubbling\.Collections')

## IMultiValueStore Interface

统一的多值存储抽象，屏蔽 List/Dictionary 等差异，同时提供冒泡事件。
Castle 动态代理的通用适配器将实现该接口。

```csharp
public interface IMultiValueStore : Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier, System.Collections.IEnumerable
```

Derived  
&#8627; [ObservableCollectionAdapter](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter')

Implements [IBubblingChangeNotifier](Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier.md 'Apq\.ChangeBubbling\.Abstractions\.IBubblingChangeNotifier'), [System\.Collections\.IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable 'System\.Collections\.IEnumerable')

| Properties | |
| :--- | :--- |
| [Count](Apq.ChangeBubbling.Collections.IMultiValueStore.Count.md 'Apq\.ChangeBubbling\.Collections\.IMultiValueStore\.Count') | 元素数量。 |
| [Items](Apq.ChangeBubbling.Collections.IMultiValueStore.Items.md 'Apq\.ChangeBubbling\.Collections\.IMultiValueStore\.Items') | 获取元素的枚举序列。 |
| [Name](Apq.ChangeBubbling.Collections.IMultiValueStore.Name.md 'Apq\.ChangeBubbling\.Collections\.IMultiValueStore\.Name') | 容器名称，作为变更的 PropertyName 以及路径段。 |
