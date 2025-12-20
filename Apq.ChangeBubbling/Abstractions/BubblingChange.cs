using System;
using System.Collections.Generic;

namespace Apq.ChangeBubbling.Abstractions;

/// <summary>
/// 承载冒泡变更的上下文信息。使用 readonly record struct 减少堆分配。
/// </summary>
public readonly record struct BubblingChange
{
    public required string PropertyName { get; init; }
    public required NodeChangeKind Kind { get; init; }

    /// <summary>
    /// 路径段列表，表示从根节点到当前节点的路径。默认为空数组。
    /// 使用 IReadOnlyList&lt;string&gt; 以支持更灵活的实现（如 ArraySegment、List 等）。
    /// </summary>
    public IReadOnlyList<string> PathSegments { get; init; } = Array.Empty<string>();

    public object? OldValue { get; init; }
    public object? NewValue { get; init; }
    public int? Index { get; init; }
    public object? Key { get; init; }

    /// <summary>
    /// 创建 BubblingChange 实例（必需的显式构造函数）。
    /// </summary>
    public BubblingChange() { }
}
