#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Nodes\.Concurrent](Apq.ChangeBubbling.Nodes.Concurrent.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent').[ConcurrentBagBubblingNode&lt;T&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>')

## ConcurrentBagBubblingNode\<T\>\.\_list Field

底层数据列表，使用锁保护。

```csharp
private readonly List<T> _list;
```

#### Field Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[T](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.T 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')