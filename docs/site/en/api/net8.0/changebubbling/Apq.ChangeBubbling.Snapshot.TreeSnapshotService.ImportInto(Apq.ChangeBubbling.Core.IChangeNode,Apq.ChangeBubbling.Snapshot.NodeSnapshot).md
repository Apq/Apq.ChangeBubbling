#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Snapshot](Apq.ChangeBubbling.Snapshot.md 'Apq\.ChangeBubbling\.Snapshot').[TreeSnapshotService](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService')

## TreeSnapshotService\.ImportInto\(IChangeNode, NodeSnapshot\) Method

将快照中的属性值写入指定节点及其子树（仅对实现 ISnapshotSerializable 的节点生效）。

```csharp
public static void ImportInto(Apq.ChangeBubbling.Core.IChangeNode target, Apq.ChangeBubbling.Snapshot.NodeSnapshot snapshot);
```
#### Parameters

<a name='Apq.ChangeBubbling.Snapshot.TreeSnapshotService.ImportInto(Apq.ChangeBubbling.Core.IChangeNode,Apq.ChangeBubbling.Snapshot.NodeSnapshot).target'></a>

`target` [IChangeNode](Apq.ChangeBubbling.Core.IChangeNode.md 'Apq\.ChangeBubbling\.Core\.IChangeNode')

<a name='Apq.ChangeBubbling.Snapshot.TreeSnapshotService.ImportInto(Apq.ChangeBubbling.Core.IChangeNode,Apq.ChangeBubbling.Snapshot.NodeSnapshot).snapshot'></a>

`snapshot` [NodeSnapshot](Apq.ChangeBubbling.Snapshot.NodeSnapshot.md 'Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot')