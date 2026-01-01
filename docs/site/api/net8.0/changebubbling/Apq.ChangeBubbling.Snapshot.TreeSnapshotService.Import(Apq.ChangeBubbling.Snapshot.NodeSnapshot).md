#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Snapshot](Apq.ChangeBubbling.Snapshot.md 'Apq\.ChangeBubbling\.Snapshot').[TreeSnapshotService](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService')

## TreeSnapshotService\.Import\(NodeSnapshot\) Method

按快照重建一棵仅含名称与层级关系的节点树。节点类型使用 ChangeNodeBase 的匿名派生实现。
若目标实现 ISnapshotSerializable，调用 ApplySnapshotProperties 回放属性值。

```csharp
public static Apq.ChangeBubbling.Core.IChangeNode Import(Apq.ChangeBubbling.Snapshot.NodeSnapshot snapshot);
```
#### Parameters

<a name='Apq.ChangeBubbling.Snapshot.TreeSnapshotService.Import(Apq.ChangeBubbling.Snapshot.NodeSnapshot).snapshot'></a>

`snapshot` [NodeSnapshot](Apq.ChangeBubbling.Snapshot.NodeSnapshot.md 'Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot')

#### Returns
[IChangeNode](Apq.ChangeBubbling.Core.IChangeNode.md 'Apq\.ChangeBubbling\.Core\.IChangeNode')