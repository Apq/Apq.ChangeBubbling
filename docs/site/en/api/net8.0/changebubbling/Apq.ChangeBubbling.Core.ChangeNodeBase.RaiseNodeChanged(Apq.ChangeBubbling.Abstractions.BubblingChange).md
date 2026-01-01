#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.RaiseNodeChanged\(BubblingChange\) Method

触发节点变更事件，并通过弱消息发布。

```csharp
protected void RaiseNodeChanged(Apq.ChangeBubbling.Abstractions.BubblingChange change);
```
#### Parameters

<a name='Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChanged(Apq.ChangeBubbling.Abstractions.BubblingChange).change'></a>

`change` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')

变更上下文。