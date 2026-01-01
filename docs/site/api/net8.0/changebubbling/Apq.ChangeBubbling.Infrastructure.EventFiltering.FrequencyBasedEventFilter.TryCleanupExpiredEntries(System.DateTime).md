#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[FrequencyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter')

## FrequencyBasedEventFilter\.TryCleanupExpiredEntries\(DateTime\) Method

尝试清理过期条目（直接遍历删除，避免临时 List 分配）。

```csharp
private void TryCleanupExpiredEntries(System.DateTime now);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.TryCleanupExpiredEntries(System.DateTime).now'></a>

`now` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')