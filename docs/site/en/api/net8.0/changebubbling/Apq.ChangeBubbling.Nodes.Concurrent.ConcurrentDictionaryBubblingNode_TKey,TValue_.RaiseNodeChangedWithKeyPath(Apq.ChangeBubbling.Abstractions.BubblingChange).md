#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent').[ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>')

## ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.RaiseNodeChangedWithKeyPath\(BubblingChange\) Method

将集合变更事件重新包装，使键直接作为属性名，跳过中间层级。

```csharp
private void RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange originalChange);
```
#### Parameters

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange).originalChange'></a>

`originalChange` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')