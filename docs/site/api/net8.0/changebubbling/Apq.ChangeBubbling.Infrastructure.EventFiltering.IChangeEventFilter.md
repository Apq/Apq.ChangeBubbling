#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering')

## IChangeEventFilter Interface

变更事件过滤器接口，提供事件过滤能力。

```csharp
public interface IChangeEventFilter
```

Derived  
&#8627; [CompositeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter')  
&#8627; [FrequencyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.FrequencyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.FrequencyBasedEventFilter')  
&#8627; [PathBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter')  
&#8627; [PropertyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter')

| Properties | |
| :--- | :--- |
| [Description](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.Description.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\.Description') | 过滤器描述。 |
| [Name](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.Name.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\.Name') | 过滤器名称。 |

| Methods | |
| :--- | :--- |
| [ShouldBubble\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.ShouldBubble(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\.ShouldBubble\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该冒泡事件。 |
| [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该处理事件。 |
