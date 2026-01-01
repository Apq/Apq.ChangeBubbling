#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PathBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter')

## PathBasedEventFilter\(string\[\], string\[\], int\) Constructor

创建基于路径的过滤器。

```csharp
public PathBasedEventFilter(string[]? allowedPaths=null, string[]? excludedPaths=null, int maxDepth=int.MaxValue);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.PathBasedEventFilter(string[],string[],int).allowedPaths'></a>

`allowedPaths` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

允许的路径

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.PathBasedEventFilter(string[],string[],int).excludedPaths'></a>

`excludedPaths` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

排除的路径

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.PathBasedEventFilter(string[],string[],int).maxDepth'></a>

`maxDepth` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

最大深度