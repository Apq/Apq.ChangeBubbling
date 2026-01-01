#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.RaiseNodeChangedBatched\(BubblingChange\) Method

内部方法：在批量模式下收集变更，否则立即触发。
派生类应使用此方法替代直接调用 RaiseNodeChanged。

```csharp
protected void RaiseNodeChangedBatched(Apq.ChangeBubbling.Abstractions.BubblingChange change);
```
#### Parameters

<a name='Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChangedBatched(Apq.ChangeBubbling.Abstractions.BubblingChange).change'></a>

`change` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')