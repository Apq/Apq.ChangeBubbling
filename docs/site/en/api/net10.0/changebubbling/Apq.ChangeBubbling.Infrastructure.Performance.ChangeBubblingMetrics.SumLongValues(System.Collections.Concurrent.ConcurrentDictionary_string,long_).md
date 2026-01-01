#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance').[ChangeBubblingMetrics](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics')

## ChangeBubblingMetrics\.SumLongValues\(ConcurrentDictionary\<string,long\>\) Method

求和 long 值（避免 LINQ 分配）。

```csharp
private static long SumLongValues(System.Collections.Concurrent.ConcurrentDictionary<string,long> dict);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.SumLongValues(System.Collections.Concurrent.ConcurrentDictionary_string,long_).dict'></a>

`dict` [System\.Collections\.Concurrent\.ConcurrentDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')

#### Returns
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')