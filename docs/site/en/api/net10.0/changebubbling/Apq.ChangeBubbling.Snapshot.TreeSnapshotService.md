#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Snapshot](Apq.ChangeBubbling.Snapshot.md 'Apq\.ChangeBubbling\.Snapshot')

## TreeSnapshotService Class

基于 IChangeNode 的快照导出与导入服务。

```csharp
public static class TreeSnapshotService
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; TreeSnapshotService

| Methods | |
| :--- | :--- |
| [Export\(IChangeNode\)](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.Export(Apq.ChangeBubbling.Core.IChangeNode).md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService\.Export\(Apq\.ChangeBubbling\.Core\.IChangeNode\)') | 从节点树导出快照：导出名称、层级；若实现 ISnapshotSerializable，则合并其属性字典到 Properties。 |
| [Import\(NodeSnapshot\)](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.Import(Apq.ChangeBubbling.Snapshot.NodeSnapshot).md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService\.Import\(Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot\)') | 按快照重建一棵仅含名称与层级关系的节点树。节点类型使用 ChangeNodeBase 的匿名派生实现。 若目标实现 ISnapshotSerializable，调用 ApplySnapshotProperties 回放属性值。 |
| [ImportInto\(IChangeNode, NodeSnapshot\)](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.ImportInto(Apq.ChangeBubbling.Core.IChangeNode,Apq.ChangeBubbling.Snapshot.NodeSnapshot).md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService\.ImportInto\(Apq\.ChangeBubbling\.Core\.IChangeNode, Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot\)') | 将快照中的属性值写入指定节点及其子树（仅对实现 ISnapshotSerializable 的节点生效）。 |
