#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.BeginBatch\(\) Method

开始批量操作。在批量操作期间，变更事件会被收集而不是立即触发。
支持嵌套调用，只有最外层的 EndBatch 才会触发事件。

```csharp
public void BeginBatch();
```