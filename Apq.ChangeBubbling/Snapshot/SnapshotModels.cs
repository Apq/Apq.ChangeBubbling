using System.Collections.Generic;

namespace Apq.ChangeBubbling.Snapshot;

/// <summary>
/// 节点快照 DTO。
/// </summary>
public sealed class NodeSnapshot
{
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, object?> Properties { get; set; } = new();
    public List<NodeSnapshot> Children { get; set; } = new();
}

/// <summary>
/// 多值容器快照 DTO。
/// </summary>
public sealed class MultiValueSnapshot
{
    public string Name { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty; // List/Dictionary
    public List<object?> ListItems { get; set; } = new();
    public Dictionary<string, object?> DictItems { get; set; } = new();
}


