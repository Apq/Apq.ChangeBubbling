using System;
using System.Collections.Generic;
using System.Linq;
using Apq.ChangeBubbling.Core;

namespace Apq.ChangeBubbling.Snapshot;

/// <summary>
/// 基于 IChangeNode 的快照导出与导入服务。
/// </summary>
public static class TreeSnapshotService
{
    /// <summary>
    /// 从节点树导出快照：导出名称、层级；若实现 ISnapshotSerializable，则合并其属性字典到 Properties。
    /// </summary>
    public static NodeSnapshot Export(IChangeNode root)
    {
        if (root is null) throw new ArgumentNullException(nameof(root));
        var snapshot = new NodeSnapshot { Name = root.Name };

        if (root is ISnapshotSerializable serializable)
        {
            var props = serializable.GetSnapshotProperties();
            if (props is not null)
            {
                foreach (var kv in props)
                {
                    snapshot.Properties[kv.Key] = kv.Value;
                }
            }
        }

        foreach (var child in root.Children)
        {
            snapshot.Children.Add(Export(child));
        }
        return snapshot;
    }

    /// <summary>
    /// 按快照重建一棵仅含名称与层级关系的节点树。节点类型使用 ChangeNodeBase 的匿名派生实现。
    /// 若目标实现 ISnapshotSerializable，调用 ApplySnapshotProperties 回放属性值。
    /// </summary>
    public static IChangeNode Import(NodeSnapshot snapshot)
    {
        if (snapshot is null) throw new ArgumentNullException(nameof(snapshot));
        var node = new SnapshotNode(snapshot.Name);
        ImportInto(node, snapshot);
        return node;
    }

    /// <summary>
    /// 将快照中的属性值写入指定节点及其子树（仅对实现 ISnapshotSerializable 的节点生效）。
    /// </summary>
    public static void ImportInto(IChangeNode target, NodeSnapshot snapshot)
    {
        if (target is null) throw new ArgumentNullException(nameof(target));
        if (snapshot is null) throw new ArgumentNullException(nameof(snapshot));

        if (target is ISnapshotSerializable serializable)
        {
            serializable.ApplySnapshotProperties(snapshot.Properties);
        }

        var nameToChild = target.Children.ToDictionary(c => c.Name, c => c);
        foreach (var childSnap in snapshot.Children)
        {
            if (!nameToChild.TryGetValue(childSnap.Name, out var childNode))
            {
                childNode = new SnapshotNode(childSnap.Name);
                target.AttachChild(childNode);
            }
            ImportInto(childNode, childSnap);
        }
    }

    private sealed class SnapshotNode : ChangeNodeBase
    {
        public SnapshotNode(string name)
        {
            Name = name;
        }
    }
}


