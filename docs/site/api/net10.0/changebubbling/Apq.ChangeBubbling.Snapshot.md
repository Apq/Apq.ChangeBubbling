#### [Apq\.ChangeBubbling](index.md 'index')

## Apq\.ChangeBubbling\.Snapshot Namespace

| Classes | |
| :--- | :--- |
| [MultiValueSnapshot](Apq.ChangeBubbling.Snapshot.MultiValueSnapshot.md 'Apq\.ChangeBubbling\.Snapshot\.MultiValueSnapshot') | 多值容器快照 DTO。 |
| [MultiValueSnapshotService](Apq.ChangeBubbling.Snapshot.MultiValueSnapshotService.md 'Apq\.ChangeBubbling\.Snapshot\.MultiValueSnapshotService') | 多值容器（List/Dictionary）与快照的互转工具。 |
| [NodeSnapshot](Apq.ChangeBubbling.Snapshot.NodeSnapshot.md 'Apq\.ChangeBubbling\.Snapshot\.NodeSnapshot') | 节点快照 DTO。 |
| [SnapshotSerializer](Apq.ChangeBubbling.Snapshot.SnapshotSerializer.md 'Apq\.ChangeBubbling\.Snapshot\.SnapshotSerializer') | 节点与多值容器的快照序列化/反序列化。 使用 System\.Text\.Json 以获得更好的性能。 |
| [TreeSnapshotService](Apq.ChangeBubbling.Snapshot.TreeSnapshotService.md 'Apq\.ChangeBubbling\.Snapshot\.TreeSnapshotService') | 基于 IChangeNode 的快照导出与导入服务。 |

| Interfaces | |
| :--- | :--- |
| [ISnapshotSerializable](Apq.ChangeBubbling.Snapshot.ISnapshotSerializable.md 'Apq\.ChangeBubbling\.Snapshot\.ISnapshotSerializable') | 约定：节点实现该接口以参与属性值的快照导出/导入。 |
