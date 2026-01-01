#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PathBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter')

## PathBasedEventFilter\.IsPathAllowed\(IReadOnlyList\<string\>\) Method

检查路径是否被允许（避免 string\.Join 分配）。

```csharp
private bool IsPathAllowed(System.Collections.Generic.IReadOnlyList<string> pathSegments);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.IsPathAllowed(System.Collections.Generic.IReadOnlyList_string_).pathSegments'></a>

`pathSegments` [System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')

路径段

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
是否被允许