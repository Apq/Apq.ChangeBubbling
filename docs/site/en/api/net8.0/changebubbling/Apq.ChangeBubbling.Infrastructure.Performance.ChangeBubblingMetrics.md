#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance')

## ChangeBubblingMetrics Class

ChangeBubbling性能指标收集器。

```csharp
public static class ChangeBubblingMetrics
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; ChangeBubblingMetrics

| Fields | |
| :--- | :--- |
| [EmaAlpha](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.EmaAlpha.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.EmaAlpha') | 指数移动平均的平滑因子 \(0\-1\)，值越大新数据权重越高。 |

| Methods | |
| :--- | :--- |
| [CalculateAverageProcessingTime\(\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.CalculateAverageProcessingTime().md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.CalculateAverageProcessingTime\(\)') | 计算平均处理时间（避免 LINQ 分配）。 |
| [CalculateEma\(TimeSpan, TimeSpan\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.CalculateEma(System.TimeSpan,System.TimeSpan).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.CalculateEma\(System\.TimeSpan, System\.TimeSpan\)') | 计算指数移动平均。 |
| [ExtractEventType\(string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.ExtractEventType(string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.ExtractEventType\(string\)') | 提取事件类型。 |
| [GetEventTypeStatistics\(\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.GetEventTypeStatistics().md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.GetEventTypeStatistics\(\)') | 获取事件类型统计。 |
| [GetMetrics\(\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.GetMetrics().md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.GetMetrics\(\)') | 获取性能指标。 |
| [GetOrCreateKey\(string, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.GetOrCreateKey(string,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.GetOrCreateKey\(string, string\)') | 获取或创建缓存的 key。 |
| [GetOrCreateKey3\(string, string, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.GetOrCreateKey3(string,string,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.GetOrCreateKey3\(string, string, string\)') | 获取或创建缓存的三段 key。 |
| [RecordEventBubbled\(string, int, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordEventBubbled(string,int,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.RecordEventBubbled\(string, int, string\)') | 记录事件冒泡。 |
| [RecordEventFiltered\(string, string, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordEventFiltered(string,string,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.RecordEventFiltered\(string, string, string\)') | 记录事件过滤。 |
| [RecordEventProcessed\(string, TimeSpan, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordEventProcessed(string,System.TimeSpan,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.RecordEventProcessed\(string, System\.TimeSpan, string\)') | 记录事件处理。 |
| [RecordNodeOperation\(string, string, TimeSpan, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordNodeOperation(string,string,System.TimeSpan,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.RecordNodeOperation\(string, string, System\.TimeSpan, string\)') | 记录节点操作。 |
| [RecordSubscriptionChanged\(string, bool, string\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.RecordSubscriptionChanged(string,bool,string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.RecordSubscriptionChanged\(string, bool, string\)') | 记录订阅变更。 |
| [Reset\(\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.Reset().md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.Reset\(\)') | 重置指标。 |
| [SumIntValues\(ConcurrentDictionary&lt;string,int&gt;\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.SumIntValues(System.Collections.Concurrent.ConcurrentDictionary_string,int_).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.SumIntValues\(System\.Collections\.Concurrent\.ConcurrentDictionary\<string,int\>\)') | 求和 int 值（避免 LINQ 分配）。 |
| [SumLongValues\(ConcurrentDictionary&lt;string,long&gt;\)](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.SumLongValues(System.Collections.Concurrent.ConcurrentDictionary_string,long_).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics\.SumLongValues\(System\.Collections\.Concurrent\.ConcurrentDictionary\<string,long\>\)') | 求和 long 值（避免 LINQ 分配）。 |
