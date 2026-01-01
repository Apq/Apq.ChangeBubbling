#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance').[ChangeBubblingMetrics](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics')

## ChangeBubblingMetrics\.RecordNodeOperation\(string, string, TimeSpan, string\) Method

记录节点操作。

```csharp
public static void RecordNodeOperation(string operation, string nodeType, System.TimeSpan processingTime, string? context=null);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordNodeOperation(string,string,System.TimeSpan,string).operation'></a>

`operation` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

操作类型

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordNodeOperation(string,string,System.TimeSpan,string).nodeType'></a>

`nodeType` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

节点类型

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordNodeOperation(string,string,System.TimeSpan,string).processingTime'></a>

`processingTime` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

处理时间

<a name='Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordNodeOperation(string,string,System.TimeSpan,string).context'></a>

`context` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

上下文