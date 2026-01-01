#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Snapshot](Apq.ChangeBubbling.Snapshot.md 'Apq\.ChangeBubbling\.Snapshot')

## ISnapshotSerializable Interface

约定：节点实现该接口以参与属性值的快照导出/导入。

```csharp
public interface ISnapshotSerializable
```

| Methods | |
| :--- | :--- |
| [ApplySnapshotProperties\(Dictionary&lt;string,object&gt;\)](Apq.ChangeBubbling.Snapshot.ISnapshotSerializable.ApplySnapshotProperties(System.Collections.Generic.Dictionary_string,object_).md 'Apq\.ChangeBubbling\.Snapshot\.ISnapshotSerializable\.ApplySnapshotProperties\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | 从快照属性字典回放属性值。 |
| [GetSnapshotProperties\(\)](Apq.ChangeBubbling.Snapshot.ISnapshotSerializable.GetSnapshotProperties().md 'Apq\.ChangeBubbling\.Snapshot\.ISnapshotSerializable\.GetSnapshotProperties\(\)') | 导出需要参与快照的属性键值对。 |
