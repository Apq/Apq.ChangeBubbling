#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core')

## ChangeNodeBase Class

提供父子管理与冒泡转译的基类。派生类可借助 Fody 自动织入属性变更。
使用弱事件订阅避免内存泄漏。线程安全。

```csharp
public abstract class ChangeNodeBase : Apq.ChangeBubbling.Core.IChangeNode, Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier, System.ComponentModel.INotifyPropertyChanged
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; ChangeNodeBase

Derived  
&#8627; [ConcurrentBagBubblingNode&lt;T&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>')  
&#8627; [ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>')  
&#8627; [DictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>')  
&#8627; [ListBubblingNode&lt;T&gt;](Apq.ChangeBubbling.Nodes.ListBubblingNode_T_.md 'Apq\.ChangeBubbling\.Nodes\.ListBubblingNode\<T\>')

Implements [IChangeNode](Apq.ChangeBubbling.Core.IChangeNode.md 'Apq\.ChangeBubbling\.Core\.IChangeNode'), [IBubblingChangeNotifier](Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier.md 'Apq\.ChangeBubbling\.Abstractions\.IBubblingChangeNotifier'), [System\.ComponentModel\.INotifyPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged 'System\.ComponentModel\.INotifyPropertyChanged')

| Properties | |
| :--- | :--- |
| [Children](Apq.ChangeBubbling.Core.ChangeNodeBase.Children.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.Children') | 子节点只读集合。 |
| [IsCoalescing](Apq.ChangeBubbling.Core.ChangeNodeBase.IsCoalescing.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.IsCoalescing') | 获取当前是否处于事件合并模式（无锁快速读取）。 |
| [IsInBatch](Apq.ChangeBubbling.Core.ChangeNodeBase.IsInBatch.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.IsInBatch') | 获取当前是否处于批量操作中（无锁快速读取）。 |
| [Name](Apq.ChangeBubbling.Core.ChangeNodeBase.Name.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.Name') | 节点名称，用于路径标识。 |
| [Parent](Apq.ChangeBubbling.Core.ChangeNodeBase.Parent.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.Parent') | 父节点，根节点为 null。 |

| Methods | |
| :--- | :--- |
| [AttachChild\(IChangeNode\)](Apq.ChangeBubbling.Core.ChangeNodeBase.AttachChild(Apq.ChangeBubbling.Core.IChangeNode).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.AttachChild\(Apq\.ChangeBubbling\.Core\.IChangeNode\)') | 附加子节点并建立事件订阅与冒泡转译。 |
| [BeginBatch\(\)](Apq.ChangeBubbling.Core.ChangeNodeBase.BeginBatch().md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.BeginBatch\(\)') | 开始批量操作。在批量操作期间，变更事件会被收集而不是立即触发。 支持嵌套调用，只有最外层的 EndBatch 才会触发事件。 |
| [BeginCoalesce\(\)](Apq.ChangeBubbling.Core.ChangeNodeBase.BeginCoalesce().md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.BeginCoalesce\(\)') | 开始事件合并模式。在此模式下，相同属性名的变更事件会被合并， 只保留最新的值（OldValue 保留第一次的，NewValue 使用最后一次的）。 适用于高频属性更新场景，减少下游处理压力。 |
| [DetachChild\(IChangeNode\)](Apq.ChangeBubbling.Core.ChangeNodeBase.DetachChild(Apq.ChangeBubbling.Core.IChangeNode).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.DetachChild\(Apq\.ChangeBubbling\.Core\.IChangeNode\)') | 移除子节点并解除订阅。 |
| [EndBatch\(bool\)](Apq.ChangeBubbling.Core.ChangeNodeBase.EndBatch(bool).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.EndBatch\(bool\)') | 结束批量操作。如果这是最外层的批量操作，则触发所有收集的变更事件。 |
| [EndCoalesce\(\)](Apq.ChangeBubbling.Core.ChangeNodeBase.EndCoalesce().md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.EndCoalesce\(\)') | 结束事件合并模式，触发所有合并后的变更事件。 |
| [FillPathFromNode\(ChangeNodeBase, string\[\], int\)](Apq.ChangeBubbling.Core.ChangeNodeBase.FillPathFromNode(Apq.ChangeBubbling.Core.ChangeNodeBase,string[],int).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.FillPathFromNode\(Apq\.ChangeBubbling\.Core\.ChangeNodeBase, string\[\], int\)') | 从节点向上填充路径数组。 |
| [GetDepth\(\)](Apq.ChangeBubbling.Core.ChangeNodeBase.GetDepth().md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.GetDepth\(\)') | 获取节点深度（从根到当前节点的路径长度）。 |
| [HandleChildNodeChangedInternal\(object, BubblingChange\)](Apq.ChangeBubbling.Core.ChangeNodeBase.HandleChildNodeChangedInternal(object,Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.HandleChildNodeChangedInternal\(object, Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 内部方法：处理子节点变更（由 WeakEventSubscription 调用）。 |
| [HandleChildPropertyChangedInternal\(object, PropertyChangedEventArgs\)](Apq.ChangeBubbling.Core.ChangeNodeBase.HandleChildPropertyChangedInternal(object,System.ComponentModel.PropertyChangedEventArgs).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.HandleChildPropertyChangedInternal\(object, System\.ComponentModel\.PropertyChangedEventArgs\)') | 内部方法：处理子节点属性变更（由 WeakEventSubscription 调用）。 |
| [InvalidateDepthCache\(\)](Apq.ChangeBubbling.Core.ChangeNodeBase.InvalidateDepthCache().md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.InvalidateDepthCache\(\)') | 使深度缓存失效（使用迭代避免深层树结构的栈溢出）。 使用 ThreadStatic 缓存的 Stack 避免每次调用都分配新对象。 |
| [RaiseNodeChanged\(BubblingChange\)](Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChanged(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.RaiseNodeChanged\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 触发节点变更事件，并通过弱消息发布。 |
| [RaiseNodeChangedBatched\(BubblingChange\)](Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChangedBatched(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.RaiseNodeChangedBatched\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 内部方法：在批量模式下收集变更，否则立即触发。 派生类应使用此方法替代直接调用 RaiseNodeChanged。 |
| [RaiseNodeChangedCoalesced\(BubblingChange\)](Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChangedCoalesced(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.RaiseNodeChangedCoalesced\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 触发节点变更事件，支持事件合并模式。 在合并模式下，相同属性名的事件会被合并。 |

| Events | |
| :--- | :--- |
| [PropertyChanged](Apq.ChangeBubbling.Core.ChangeNodeBase.PropertyChanged.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase\.PropertyChanged') | 由 Fody 自动织入：属性变更事件。 |
