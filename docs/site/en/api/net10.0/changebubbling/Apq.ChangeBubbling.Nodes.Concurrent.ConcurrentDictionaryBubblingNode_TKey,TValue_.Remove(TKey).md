#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent').[ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>')

## ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.Remove\(TKey\) Method

移除键（通过代理触发集合更改）。使用原子操作避免竞态条件。

```csharp
public bool Remove(TKey key);
```
#### Parameters

<a name='Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.Remove(TKey).key'></a>

`key` [TKey](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TKey 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TKey')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')