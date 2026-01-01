#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance').[ChangeBubblingMetrics](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics')

## ChangeBubblingMetrics\.SumIntValues\(ConcurrentDictionary\<string,int\>\) Method

求和 int 值（避免 LINQ 分配）。

```csharp
private static int SumIntValues(System.Collections.Concurrent.ConcurrentDictionary<string,int> dict);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.SumIntValues(System.Collections.Concurrent.ConcurrentDictionary_string,int_).dict'></a>

`dict` [System\.Collections\.Concurrent\.ConcurrentDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')