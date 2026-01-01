#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Abstractions](Apq.ChangeBubbling.Abstractions.md 'Apq\.ChangeBubbling\.Abstractions')

## BubblingChange Struct

承载冒泡变更的上下文信息。使用 readonly record struct 减少堆分配。

```csharp
public readonly record struct BubblingChange : System.IEquatable<Apq.ChangeBubbling.Abstractions.BubblingChange>
```

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [BubblingChange\(\)](Apq.ChangeBubbling.Abstractions.BubblingChange.BubblingChange().md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange\.BubblingChange\(\)') | 创建 BubblingChange 实例（必需的显式构造函数）。 |

| Properties | |
| :--- | :--- |
| [PathSegments](Apq.ChangeBubbling.Abstractions.BubblingChange.PathSegments.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange\.PathSegments') | 路径段列表，表示从根节点到当前节点的路径。默认为空数组。 使用 IReadOnlyList\<string\> 以支持更灵活的实现（如 ArraySegment、List 等）。 |
