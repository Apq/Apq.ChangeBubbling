#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes](Apq.ChangeBubbling.Nodes.md 'Apq\.ChangeBubbling\.Nodes')

## DictionaryBubblingNode\<TKey,TValue\> Class

基于 [System\.Collections\.IDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.idictionary 'System\.Collections\.IDictionary') 的更改事件冒泡节点。
使用 [ObservableCollectionAdapter](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter') 代理底层集合，
将 Add/Remove/Indexer 等操作映射为 [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange') 并向上冒泡。
适用于任意 [TValue](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.TValue 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.TValue') 类型（无需实现 IChangeNode）。

注意：此类非线程安全。如需线程安全版本，请使用 [ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>')。

```csharp
public class DictionaryBubblingNode<TKey,TValue> : Apq.ChangeBubbling.Core.ChangeNodeBase
    where TKey : notnull
```
#### Type parameters

<a name='Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.TKey'></a>

`TKey`

<a name='Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.TValue'></a>

`TValue`

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase') &#129106; DictionaryBubblingNode\<TKey,TValue\>

| Constructors | |
| :--- | :--- |
| [DictionaryBubblingNode\(string\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.DictionaryBubblingNode(string).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.DictionaryBubblingNode\(string\)') | 创建节点。 |

| Fields | |
| :--- | :--- |
| [Adapter](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Adapter.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Adapter') | 集合变更适配器。 |
| [Data](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Data.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Data') | 底层数据字典。 |

| Properties | |
| :--- | :--- |
| [Count](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Count.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Count') | 当前元素数量（直接访问底层集合，无需快照）。 |
| [Items](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Items.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Items') | 当前项集合的快照（缓存，集合变更时自动失效）。 |
| [Keys](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Keys.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Keys') | 当前键集合的快照（缓存，集合变更时自动失效）。 |

| Methods | |
| :--- | :--- |
| [HandleAdapterNodeChanged\(object, BubblingChange\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.HandleAdapterNodeChanged(object,Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.HandleAdapterNodeChanged\(object, Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 处理适配器的节点变更事件（避免 Lambda 闭包分配）。 |
| [InvalidateSnapshots\(\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.InvalidateSnapshots().md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.InvalidateSnapshots\(\)') | 使快照缓存失效。 |
| [PopulateSilently\(IEnumerable&lt;KeyValuePair&lt;TKey,TValue&gt;&gt;\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.PopulateSilently(System.Collections.Generic.IEnumerable_System.Collections.Generic.KeyValuePair_TKey,TValue__).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.PopulateSilently\(System\.Collections\.Generic\.IEnumerable\<System\.Collections\.Generic\.KeyValuePair\<TKey,TValue\>\>\)') | 静默批量填充（不经过代理，不产生集合事件），适合初始化/克隆。 |
| [Put\(TKey, TValue\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Put(TKey,TValue).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Put\(TKey, TValue\)') | 设置或新增键值（通过代理触发集合更改）。 |
| [RaiseNodeChangedWithKeyPath\(BubblingChange\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.RaiseNodeChangedWithKeyPath\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 将集合变更事件重新包装，使键直接作为属性名，跳过中间层级。 |
| [Remove\(TKey\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.Remove(TKey).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.Remove\(TKey\)') | 移除键（通过代理触发集合更改）。 |
| [TryGet\(TKey, TValue\)](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.TryGet(TKey,TValue).md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>\.TryGet\(TKey, TValue\)') | 尝试读取。 |
