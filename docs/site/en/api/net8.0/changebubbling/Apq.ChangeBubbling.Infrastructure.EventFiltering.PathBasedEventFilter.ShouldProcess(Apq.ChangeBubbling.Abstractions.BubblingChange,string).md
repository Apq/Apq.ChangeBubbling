#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PathBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter')

## PathBasedEventFilter\.ShouldProcess\(BubblingChange, string\) Method

判断是否应该处理事件。

```csharp
public bool ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange change, string? context=null);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).change'></a>

`change` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')

变更事件

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).context'></a>

`context` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

上下文

Implements [ShouldProcess\(BubblingChange, string\)](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.ShouldProcess(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\.ShouldProcess\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
是否应该处理