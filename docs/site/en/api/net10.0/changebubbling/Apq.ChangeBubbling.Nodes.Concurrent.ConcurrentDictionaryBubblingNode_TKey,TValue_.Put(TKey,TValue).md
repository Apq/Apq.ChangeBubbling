#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent').[ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>')

## ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Put\(TKey, TValue\) Method

设置或新增键值（使用原子操作，正确区分 Add 和 Update）。

```csharp
public void Put(TKey key, TValue value);
```
#### Parameters

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Put(TKey,TValue).key'></a>

`key` [TKey](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TKey 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TKey')

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Put(TKey,TValue).value'></a>

`value` [TValue](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TValue 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TValue')