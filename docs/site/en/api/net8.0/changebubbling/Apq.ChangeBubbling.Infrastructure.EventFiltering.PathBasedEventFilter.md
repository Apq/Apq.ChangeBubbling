#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering')

## PathBasedEventFilter Class

基于路径的变更事件过滤器。

```csharp
public class PathBasedEventFilter : Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; PathBasedEventFilter

Implements [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')

| Constructors | |
| :--- | :--- |
| [PathBasedEventFilter\(string\[\], string\[\], int\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.PathBasedEventFilter(string[],string[],int).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.PathBasedEventFilter\(string\[\], string\[\], int\)') | 创建基于路径的过滤器。 |

| Properties | |
| :--- | :--- |
| [Description](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.Description.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.Description') | 过滤器描述。 |
| [Name](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.Name.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.Name') | 过滤器名称。 |

| Methods | |
| :--- | :--- |
| [IsPathAllowed\(IReadOnlyList&lt;string&gt;\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.IsPathAllowed(System.Collections.Generic.IReadOnlyList_string_).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.IsPathAllowed\(System\.Collections\.Generic\.IReadOnlyList\<string\>\)') | 检查路径是否被允许（避免 string\.Join 分配）。 |
| [MatchesPath\(IReadOnlyList&lt;string&gt;, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.MatchesPath(System.Collections.Generic.IReadOnlyList_string_,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.MatchesPath\(System\.Collections\.Generic\.IReadOnlyList\<string\>, string\)') | 检查路径段是否匹配指定模式（避免 string\.Join）。 |
| [ShouldBubble\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.ShouldBubble(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.ShouldBubble\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该冒泡事件。 |
| [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 判断是否应该处理事件。 |
