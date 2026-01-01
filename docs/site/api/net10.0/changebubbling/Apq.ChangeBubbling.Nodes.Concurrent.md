#### [Apq\.ChangeBubbling](index.md 'index')

## Apq\.ChangeBubbling\.Nodes\.Concurrent Namespace

| Classes | |
| :--- | :--- |
| [ConcurrentBagBubblingNode&lt;T&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>') | 基于线程安全列表的更改事件冒泡节点。 将 Add/Insert/Remove/Indexer 等操作映射为 [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange') 并向上冒泡。 适用于任意 [T](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentBagBubblingNode_T_.T 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentBagBubblingNode\<T\>\.T') 类型（无需实现 IChangeNode）。 所有操作都是线程安全的。 |
| [ConcurrentDictionaryBubblingNode&lt;TKey,TValue&gt;](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>') | 基于 [ConcurrentDictionary](https://learn.microsoft.com/en-us/dotnet/api/concurrentdictionary 'ConcurrentDictionary') 的线程安全更改事件冒泡节点。 直接使用 ConcurrentDictionary 的原子操作，手动触发变更事件。 适用于任意 [TValue](Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.md#Apq.ChangeBubbling.Nodes.Concurrent.ConcurrentDictionaryBubblingNode_TKey,TValue_.TValue 'Apq\.ChangeBubbling\.Nodes\.Concurrent\.ConcurrentDictionaryBubblingNode\<TKey,TValue\>\.TValue') 类型（无需实现 IChangeNode）。 所有操作都是线程安全的。 |
