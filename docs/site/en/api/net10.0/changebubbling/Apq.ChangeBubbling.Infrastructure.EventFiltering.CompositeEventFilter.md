#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering')

## CompositeEventFilter Class

组合变更事件过滤器。

```csharp
public class CompositeEventFilter : Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; CompositeEventFilter

Implements [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')

| Constructors | |
| :--- | :--- |
| [CompositeEventFilter\(IChangeEventFilter\[\], FilterMode\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.CompositeEventFilter(Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter[],Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.CompositeEventFilter\(Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\[\], Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.FilterMode\)') | 创建组合过滤器。 |

| Properties | |
| :--- | :--- |
| [Description](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.Description.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.Description') | 过滤器描述。 |
| [Name](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.Name.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.Name') | 过滤器名称。 |

| Methods | |
| :--- | :--- |
| [ShouldBubble\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.ShouldBubble(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.ShouldBubble\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该冒泡事件。 |
| [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该处理事件。 |
