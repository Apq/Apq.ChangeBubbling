#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.BeginCoalesce\(\) Method

开始事件合并模式。在此模式下，相同属性名的变更事件会被合并，
只保留最新的值（OldValue 保留第一次的，NewValue 使用最后一次的）。
适用于高频属性更新场景，减少下游处理压力。

```csharp
public void BeginCoalesce();
```