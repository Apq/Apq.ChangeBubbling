#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering')

## PropertyBasedEventFilter Class

基于属性的变更事件过滤器。

```csharp
public class PropertyBasedEventFilter : Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; PropertyBasedEventFilter

Implements [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')

| Constructors | |
| :--- | :--- |
| [PropertyBasedEventFilter\(string\[\], string\[\], NodeChangeKind\[\], NodeChangeKind\[\], bool\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.PropertyBasedEventFilter\(string\[\], string\[\], Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind\[\], Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind\[\], bool\)') | 创建基于属性的过滤器。 |

| Properties | |
| :--- | :--- |
| [Description](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.Description.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.Description') | 过滤器描述。 |
| [Name](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.Name.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.Name') | 过滤器名称。 |

| Methods | |
| :--- | :--- |
| [IsKindAllowed\(NodeChangeKind\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.IsKindAllowed(Apq.ChangeBubbling.Abstractions.NodeChangeKind).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.IsKindAllowed\(Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind\)') | 检查变更类型是否被允许。 |
| [IsPropertyAllowed\(string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.IsPropertyAllowed(string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.IsPropertyAllowed\(string\)') | 检查属性是否被允许。 |
| [ShouldBubble\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.ShouldBubble(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.ShouldBubble\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该冒泡事件。 |
| [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该处理事件。 |
