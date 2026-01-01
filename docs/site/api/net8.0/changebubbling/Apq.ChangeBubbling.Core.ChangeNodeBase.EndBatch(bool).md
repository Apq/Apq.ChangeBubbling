#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.EndBatch\(bool\) Method

结束批量操作。如果这是最外层的批量操作，则触发所有收集的变更事件。

```csharp
public void EndBatch(bool raiseAggregated=true);
```
#### Parameters

<a name='Apq.ChangeBubbling.Core.ChangeNodeBase.EndBatch(bool).raiseAggregated'></a>

`raiseAggregated` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

是否触发聚合事件（默认 true）。