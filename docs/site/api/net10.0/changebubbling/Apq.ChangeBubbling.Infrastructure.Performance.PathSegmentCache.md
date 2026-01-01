#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Performance](Apq.ChangeBubbling.Infrastructure.Performance.md 'Apq\.ChangeBubbling\.Infrastructure\.Performance')

## PathSegmentCache Class

PathSegments 单元素数组缓存，避免高频场景下重复分配。

```csharp
internal static class PathSegmentCache
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; PathSegmentCache

| Methods | |
| :--- | :--- |
| [Clear\(\)](Apq.ChangeBubbling.Infrastructure.Performance.PathSegmentCache.Clear().md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.PathSegmentCache\.Clear\(\)') | 清除缓存（用于测试）。 |
| [GetIndexSegment\(int\)](Apq.ChangeBubbling.Infrastructure.Performance.PathSegmentCache.GetIndexSegment(int).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.PathSegmentCache\.GetIndexSegment\(int\)') | 获取索引单元素 PathSegments 数组（缓存）。 |
| [GetIndexString\(int\)](Apq.ChangeBubbling.Infrastructure.Performance.PathSegmentCache.GetIndexString(int).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.PathSegmentCache\.GetIndexString\(int\)') | 获取索引格式字符串 "\[index\]"（缓存常用索引）。 |
| [GetSingle\(string\)](Apq.ChangeBubbling.Infrastructure.Performance.PathSegmentCache.GetSingle(string).md 'Apq\.ChangeBubbling\.Infrastructure\.Performance\.PathSegmentCache\.GetSingle\(string\)') | 获取单元素 PathSegments 数组（缓存）。 |
