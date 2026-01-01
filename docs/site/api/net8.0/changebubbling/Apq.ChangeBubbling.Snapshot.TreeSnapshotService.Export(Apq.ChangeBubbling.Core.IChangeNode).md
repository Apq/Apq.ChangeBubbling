#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Snapshot](Apq.ChangeBubbling.Snapshot.md 'Apq\.ChangeBubbling\.Snapshot').[TreeSnapshotService](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService')

## TreeSnapshotService\.Export\(IChangeNode\) Method

从节点树导出快照：导出名称、层级；若实现 ISnapshotSerializable，则合并其属性字典到 Properties。

```csharp
public static Apq.ChangeBubbling.Snapshot.NodeSnapshot Export(Apq.ChangeBubbling.Core.IChangeNode root);
```
#### Parameters

<a name='Apq.ChangeBubbling.Snapshot.TreeSnapshotService.Export(Apq.ChangeBubbling.Core.IChangeNode).root'></a>

`root` [IChangeNode](Apq.ChangeBubbling.Core.IChangeNode.md 'Apq\.ChangeBubbling\.Core\.IChangeNode')

#### Returns
[NodeSnapshot](Apq.ChangeBubbling.Snapshot.NodeSnapshot.md 'Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot')