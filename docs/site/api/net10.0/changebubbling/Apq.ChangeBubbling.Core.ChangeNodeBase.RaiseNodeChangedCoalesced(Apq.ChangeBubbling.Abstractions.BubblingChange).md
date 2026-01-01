#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.RaiseNodeChangedCoalesced\(BubblingChange\) Method

触发节点变更事件，支持事件合并模式。
在合并模式下，相同属性名的事件会被合并。

```csharp
protected void RaiseNodeChangedCoalesced(Apq.ChangeBubbling.Abstractions.BubblingChange change);
```
#### Parameters

<a name='Apq.ChangeBubbling.Core.ChangeNodeBase.RaiseNodeChangedCoalesced(Apq.ChangeBubbling.Abstractions.BubblingChange).change'></a>

`change` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')

变更上下文。