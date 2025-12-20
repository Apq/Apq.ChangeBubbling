using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Apq.ChangeBubbling.Snapshot;

/// <summary>
/// 节点与多值容器的快照序列化/反序列化。
/// 使用 System.Text.Json 以获得更好的性能。
/// </summary>
public static class SnapshotSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
    };

    public static string ToJson(NodeSnapshot snapshot) => JsonSerializer.Serialize(snapshot, Options);
    public static NodeSnapshot FromJson(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json)) return new NodeSnapshot();
            return JsonSerializer.Deserialize<NodeSnapshot>(json, Options) ?? new NodeSnapshot();
        }
        catch
        {
            return new NodeSnapshot();
        }
    }

    public static string ToJson(MultiValueSnapshot snapshot) => JsonSerializer.Serialize(snapshot, Options);
    public static MultiValueSnapshot FromJsonMulti(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json)) return new MultiValueSnapshot();
            return JsonSerializer.Deserialize<MultiValueSnapshot>(json, Options) ?? new MultiValueSnapshot();
        }
        catch
        {
            return new MultiValueSnapshot();
        }
    }

    public static T[] ToArray<T>(IEnumerable<T>? items)
    {
        if (items is null) return Array.Empty<T>();
        if (items is T[] arr) return arr;
        if (items is List<T> list) return list.ToArray();
        return new List<T>(items).ToArray();
    }
}
