#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Abstractions](Apq.ChangeBubbling.Abstractions.md 'Apq\.ChangeBubbling\.Abstractions').[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')

## BubblingChange\.PathSegments Property

路径段列表，表示从根节点到当前节点的路径。默认为空数组。
使用 IReadOnlyList\<string\> 以支持更灵活的实现（如 ArraySegment、List 等）。

```csharp
public System.Collections.Generic.IReadOnlyList<string> PathSegments { get; init; }
```

#### Property Value
[System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')