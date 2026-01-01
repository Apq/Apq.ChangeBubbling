#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes](Apq.ChangeBubbling.Nodes.md 'Apq\.ChangeBubbling\.Nodes').[DictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.DictionaryBubblingNode\<TKey,TValue\>')

## DictionaryBubblingNode\<TKey,TValue\>\.RaiseNodeChangedWithKeyPath\(BubblingChange\) Method

将集合变更事件重新包装，使键直接作为属性名，跳过中间层级。

```csharp
private void RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange originalChange);
```
#### Parameters

<a name='Apq.ChangeBubbling.Nodes.DictionaryBubblingNode_TKey,TValue_.RaiseNodeChangedWithKeyPath(Apq.ChangeBubbling.Abstractions.BubblingChange).originalChange'></a>

`originalChange` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')