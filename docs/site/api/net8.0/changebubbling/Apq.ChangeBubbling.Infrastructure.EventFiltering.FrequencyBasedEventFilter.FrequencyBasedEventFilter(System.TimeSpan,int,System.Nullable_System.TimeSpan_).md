#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[FrequencyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter')

## FrequencyBasedEventFilter\(TimeSpan, int, Nullable\<TimeSpan\>\) Constructor

创建基于频率的过滤器。

```csharp
public FrequencyBasedEventFilter(System.TimeSpan throttleInterval, int maxEntries=10000, System.Nullable<System.TimeSpan> cleanupThreshold=null);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.FrequencyBasedEventFilter(System.TimeSpan,int,System.Nullable_System.TimeSpan_).throttleInterval'></a>

`throttleInterval` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

节流间隔

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.FrequencyBasedEventFilter(System.TimeSpan,int,System.Nullable_System.TimeSpan_).maxEntries'></a>

`maxEntries` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

最大条目数（默认 10000）

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.FrequencyBasedEventFilter(System.TimeSpan,int,System.Nullable_System.TimeSpan_).cleanupThreshold'></a>

`cleanupThreshold` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

过期清理阈值（默认为节流间隔的 10 倍）