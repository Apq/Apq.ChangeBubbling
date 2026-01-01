#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PropertyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter')

## PropertyBasedEventFilter\.IsKindAllowed\(NodeChangeKind\) Method

检查变更类型是否被允许。

```csharp
private bool IsKindAllowed(Apq.ChangeBubbling.Abstractions.NodeChangeKind kind);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.IsKindAllowed(Apq.ChangeBubbling.Abstractions.NodeChangeKind).kind'></a>

`kind` [NodeChangeKind](Apq.ChangeBubbling.Abstractions.NodeChangeKind.md 'Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind')

变更类型

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
是否被允许