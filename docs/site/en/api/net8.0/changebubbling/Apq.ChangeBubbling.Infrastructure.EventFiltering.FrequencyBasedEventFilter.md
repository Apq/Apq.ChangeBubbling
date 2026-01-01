#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering')

## FrequencyBasedEventFilter Class

基于频率的变更事件过滤器（线程安全）。
自动清理过期条目，防止内存泄漏。

```csharp
public class FrequencyBasedEventFilter : Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; FrequencyBasedEventFilter

Implements [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')

| Constructors | |
| :--- | :--- |
| [FrequencyBasedEventFilter\(TimeSpan, int, Nullable&lt;TimeSpan&gt;\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.FrequencyBasedEventFilter(System.TimeSpan,int,System.Nullable_System.TimeSpan_).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.FrequencyBasedEventFilter\(System\.TimeSpan, int, System\.Nullable\<System\.TimeSpan\>\)') | 创建基于频率的过滤器。 |

| Properties | |
| :--- | :--- |
| [Description](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.Description.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.Description') | 过滤器描述。 |
| [Name](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.Name.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.Name') | 过滤器名称。 |

| Methods | |
| :--- | :--- |
| [CleanupExpiredEntries\(\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.CleanupExpiredEntries().md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.CleanupExpiredEntries\(\)') | 手动清理所有过期条目。 |
| [GetOrCreateKey\(string, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.GetOrCreateKey(string,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.GetOrCreateKey\(string, string\)') | 获取或创建缓存的 key。 |
| [Reset\(\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.Reset().md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.Reset\(\)') | 重置过滤器状态。 |
| [ShouldBubble\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.ShouldBubble(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.ShouldBubble\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该冒泡事件。 |
| [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该处理事件。 |
| [TryCleanupExpiredEntries\(DateTime\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.TryCleanupExpiredEntries(System.DateTime).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter\.TryCleanupExpiredEntries\(System\.DateTime\)') | 尝试清理过期条目（直接遍历删除，避免临时 List 分配）。 |
