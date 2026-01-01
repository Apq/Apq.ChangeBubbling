#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent')

## ConcurrentDictionaryBubblingNode\<TKey,TValue\> Class

基于 [ConcurrentDictionary](https://learn.microsoft.com/en-us/dotnet/api/concurrentdictionary 'ConcurrentDictionary') 的线程安全更改事件冒泡节点。
直接使用 ConcurrentDictionary 的原子操作，手动触发变更事件。
适用于任意 [TValue](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TValue 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TValue') 类型（无需实现 IChangeNode）。
所有操作都是线程安全的。

```csharp
public class ConcurrentDictionaryBubblingNode<TKey,TValue> : Apq.ChangeBubbling.Core.ChangeNodeBase
    where TKey : notnull
```
#### Type parameters

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TKey'></a>

`TKey`

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TValue'></a>

`TValue`

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase') &#129106; ConcurrentDictionaryBubblingNode\<TKey,TValue\>

| Constructors | |
| :--- | :--- |
| [ConcurrentDictionaryBubblingNode\(string\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.ConcurrentDictionaryBubblingNode(string).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.ConcurrentDictionaryBubblingNode\(string\)') | 创建线程安全节点。 |

| Fields | |
| :--- | :--- |
| [Data](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Data.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Data') | 底层线程安全数据字典。 |

| Properties | |
| :--- | :--- |
| [Count](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Count.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Count') | 当前元素数量（无锁快速路径）。 |
| [Items](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Items.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Items') | 当前项集合的快照（缓存，集合变更时自动失效，线程安全）。 |
| [Keys](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Keys.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Keys') | 当前键集合的快照（缓存，集合变更时自动失效，线程安全）。 |

| Methods | |
| :--- | :--- |
| [InvalidateSnapshots\(\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.InvalidateSnapshots().md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.InvalidateSnapshots\(\)') | 使快照缓存失效。 |
| [PopulateSilently\(IEnumerable&lt;KeyValuePair&lt;TKey,TValue&gt;&gt;\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.PopulateSilently(System.Collections.Generic.IEnumerable_System.Collections.Generic.KeyValuePair_TKey,TValue__).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.PopulateSilently\(System\.Collections\.Generic\.IEnumerable\<System\.Collections\.Generic\.KeyValuePair\<TKey,TValue\>\>\)') | 静默批量填充（不产生集合事件），适合初始化/克隆。 |
| [Put\(TKey, TValue\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Put(TKey,TValue).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Put\(TKey, TValue\)') | 设置或新增键值（使用原子操作，正确区分 Add 和 Update）。 |
| [RaiseNodeChangedWithKeyPath\(BubblingChange\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.RaiseNodeChangedWithKeyPath\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 将集合变更事件重新包装，使键直接作为属性名，跳过中间层级。 |
| [Remove\(TKey\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Remove(TKey).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Remove\(TKey\)') | 移除键（通过代理触发集合更改）。使用原子操作避免竞态条件。 |
| [TryGet\(TKey, TValue\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TryGet(TKey,TValue).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TryGet\(TKey, TValue\)') | 尝试读取。 |
