#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PropertyBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PropertyBasedEventFilter')

## PropertyBasedEventFilter\(string\[\], string\[\], NodeChangeKind\[\], NodeChangeKind\[\], bool\) Constructor

创建基于属性的过滤器。

```csharp
public PropertyBasedEventFilter(string[]? allowedProperties=null, string[]? excludedProperties=null, Apq.ChangeBubbling.Abstractions.NodeChangeKind[]? allowedKinds=null, Apq.ChangeBubbling.Abstractions.NodeChangeKind[]? excludedKinds=null, bool useExactMatch=false);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).allowedProperties'></a>

`allowedProperties` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

允许的属性

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).excludedProperties'></a>

`excludedProperties` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

排除的属性

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).allowedKinds'></a>

`allowedKinds` [NodeChangeKind](Apq.ChangeBubbling.Abstractions.NodeChangeKind.md 'Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

允许的变更类型

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).excludedKinds'></a>

`excludedKinds` [NodeChangeKind](Apq.ChangeBubbling.Abstractions.NodeChangeKind.md 'Apq\.ChangeBubbling\.Abstractions\.NodeChangeKind')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

排除的变更类型

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PropertyBasedEventFilter.PropertyBasedEventFilter(string[],string[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],Apq.ChangeBubbling.Abstractions.NodeChangeKind[],bool).useExactMatch'></a>

`useExactMatch` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

是否使用精确匹配（默认 false 为模糊匹配）