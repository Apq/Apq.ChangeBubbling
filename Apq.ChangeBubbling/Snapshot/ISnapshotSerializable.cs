using System.Collections.Generic;

namespace Apq.ChangeBubbling.Snapshot;

/// <summary>
/// 约定：节点实现该接口以参与属性值的快照导出/导入。
/// </summary>
public interface ISnapshotSerializable
{
    /// <summary>
    /// 导出需要参与快照的属性键值对。
    /// </summary>
    Dictionary<string, object?> GetSnapshotProperties();

    /// <summary>
    /// 从快照属性字典回放属性值。
    /// </summary>
    void ApplySnapshotProperties(Dictionary<string, object?> properties);
}


