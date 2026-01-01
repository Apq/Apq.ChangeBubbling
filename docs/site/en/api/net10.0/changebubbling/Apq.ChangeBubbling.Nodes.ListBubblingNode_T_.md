#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes](Apq.ChangeBubbling.Nodes.md 'Apq\.ChangeBubbling\.Nodes')

## ListBubblingNode\<T\> Class

基于 [System\.Collections\.IList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ilist 'System\.Collections\.IList') 的更改事件冒泡节点。
使用 [ObservableCollectionAdapter](Apq.ChangeBubbling.Collections.ObservableCollectionAdapter.md 'Apq\.ChangeBubbling\.Collections\.ObservableCollectionAdapter') 代理底层集合，
将 Add/Insert/Remove/Indexer 等操作映射为 [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange') 并向上冒泡。
适用于任意 [T](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.md#Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.T 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.T') 类型（无需实现 IChangeNode）。

注意：此类非线程安全。如需线程安全版本，请使用 [ConcurrentBagBubblingNode&lt;T&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>')。

```csharp
public class ListBubblingNode<T> : Apq.ChangeBubbling.Core.ChangeNodeBase
```
#### Type parameters

<a name='Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.T'></a>

`T`

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase') &#129106; ListBubblingNode\<T\>

| Constructors | |
| :--- | :--- |
| [ListBubblingNode\(string\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.ListBubblingNode(string).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.ListBubblingNode\(string\)') | 创建节点。 |

| Fields | |
| :--- | :--- |
| [Adapter](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Adapter.md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Adapter') | 集合变更适配器。 |
| [Data](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Data.md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Data') | 底层数据列表。 |

| Properties | |
| :--- | :--- |
| [Count](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Count.md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Count') | 当前元素数量（直接访问底层集合，无需快照）。 |
| [Items](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Items.md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Items') | 当前项集合的快照（缓存，集合变更时自动失效）。 |

| Methods | |
| :--- | :--- |
| [Add\(T\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Add(T).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Add\(T\)') | 在末尾添加（通过代理触发集合更改）。 |
| [HandleAdapterNodeChanged\(object, BubblingChange\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.HandleAdapterNodeChanged(object,Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.HandleAdapterNodeChanged\(object, Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 处理适配器的节点变更事件（避免 Lambda 闭包分配）。 |
| [Insert\(int, T\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Insert(int,T).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Insert\(int, T\)') | 在指定位置插入（通过代理触发集合更改）。 |
| [InvalidateItemsSnapshot\(\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.InvalidateItemsSnapshot().md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.InvalidateItemsSnapshot\(\)') | 使快照缓存失效。 |
| [PopulateSilently\(IEnumerable&lt;T&gt;\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.PopulateSilently(System.Collections.Generic.IEnumerable_T_).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.PopulateSilently\(System\.Collections\.Generic\.IEnumerable\<T\>\)') | 静默批量填充（不经过代理，不产生集合事件），适合初始化/克隆。 |
| [RaiseNodeChangedWithIndexPath\(BubblingChange\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.RaiseNodeChangedWithIndexPath(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.RaiseNodeChangedWithIndexPath\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 将集合变更事件重新包装，使索引直接作为属性名，跳过中间层级。 |
| [Remove\(T\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.Remove(T).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.Remove\(T\)') | 按值移除（通过代理触发集合更改）。 |
| [RemoveAt\(int\)](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.RemoveAt(int).md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>\.RemoveAt\(int\)') | 按索引移除（通过代理触发集合更改）。 |
