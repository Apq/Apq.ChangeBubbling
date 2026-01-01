#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[PathBasedEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.PathBasedEventFilter')

## PathBasedEventFilter\.MatchesPath\(IReadOnlyList\<string\>, string\) Method

检查路径段是否匹配指定模式（避免 string\.Join）。

```csharp
private static bool MatchesPath(System.Collections.Generic.IReadOnlyList<string>? pathSegments, string pattern);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.MatchesPath(System.Collections.Generic.IReadOnlyList_string_,string).pathSegments'></a>

`pathSegments` [System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.PathBasedEventFilter.MatchesPath(System.Collections.Generic.IReadOnlyList_string_,string).pattern'></a>

`pattern` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')