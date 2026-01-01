#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance').[ChangeBubblingMetrics](Apq.ChangeBubbling.Infrastructure.Performance.ChangeBubblingMetrics.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.ChangeBubblingMetrics')

## ChangeBubblingMetrics\.CalculateAverageProcessingTime\(\) Method

计算平均处理时间（避免 LINQ 分配）。

```csharp
private static System.TimeSpan CalculateAverageProcessingTime();
```

#### Returns
[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')  
平均处理时间