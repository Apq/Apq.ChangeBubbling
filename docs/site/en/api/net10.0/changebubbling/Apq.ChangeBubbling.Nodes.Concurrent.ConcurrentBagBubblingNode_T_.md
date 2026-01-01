#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent')

## ConcurrentBagBubblingNode\<T\> Class

基于线程安全列表的更改事件冒泡节点。
将 Add/Insert/Remove/Indexer 等操作映射为 [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange') 并向上冒泡。
适用于任意 [T](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.T 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.T') 类型（无需实现 IChangeNode）。
所有操作都是线程安全的。

```csharp
public class ConcurrentBagBubblingNode<T> : Apq.ChangeBubbling.Core.ChangeNodeBase
```
#### Type parameters

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.T'></a>

`T`

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase') &#129106; ConcurrentBagBubblingNode\<T\>

| Constructors | |
| :--- | :--- |
| [ConcurrentBagBubblingNode\(string\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.ConcurrentBagBubblingNode(string).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.ConcurrentBagBubblingNode\(string\)') | 创建线程安全节点。 |

| Fields | |
| :--- | :--- |
| [\_list](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_._list.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.\_list') | 底层数据列表，使用锁保护。 |

| Properties | |
| :--- | :--- |
| [Count](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Count.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Count') | 当前元素数量。 |
| [Items](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Items.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Items') | 当前项集合的快照（缓存，集合变更时自动失效）。 |

| Methods | |
| :--- | :--- |
| [Add\(T\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Add(T).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Add\(T\)') | 在末尾添加。 |
| [Clear\(\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Clear().md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Clear\(\)') | 清空所有元素。 |
| [Insert\(int, T\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Insert(int,T).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Insert\(int, T\)') | 在指定位置插入。 |
| [PopulateSilently\(IEnumerable&lt;T&gt;\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.PopulateSilently(System.Collections.Generic.IEnumerable_T_).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.PopulateSilently\(System\.Collections\.Generic\.IEnumerable\<T\>\)') | 静默批量填充（不产生集合事件），适合初始化/克隆。 |
| [RaiseNodeChangedWithIndexPath\(BubblingChange\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.RaiseNodeChangedWithIndexPath(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.RaiseNodeChangedWithIndexPath\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 将集合变更事件重新包装，使索引直接作为属性名，跳过中间层级。 |
| [Remove\(T\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.Remove(T).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.Remove\(T\)') | 按值移除。 |
| [RemoveAt\(int\)](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.RemoveAt(int).md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.RemoveAt\(int\)') | 按索引移除。 |
