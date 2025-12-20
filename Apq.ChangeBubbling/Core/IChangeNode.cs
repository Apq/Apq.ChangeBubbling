using System.ComponentModel;
using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Core;

/// <summary>
/// 定义具有父子关系、支持冒泡通知的节点接口。
/// </summary>
public interface IChangeNode : IBubblingChangeNotifier, INotifyPropertyChanged
{
    /// <summary>
    /// 节点名称，用于路径标识。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 父节点，根节点为 null。
    /// </summary>
    IChangeNode? Parent { get; }

    /// <summary>
    /// 子节点只读集合。
    /// </summary>
    IReadOnlyList<IChangeNode> Children { get; }

    /// <summary>
    /// 附加子节点并建立事件订阅与冒泡转译。
    /// </summary>
    /// <param name="child">子节点。</param>
    void AttachChild(IChangeNode child);

    /// <summary>
    /// 移除子节点并解除订阅。
    /// </summary>
    /// <param name="child">子节点。</param>
    void DetachChild(IChangeNode child);
}


