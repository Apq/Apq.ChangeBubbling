using System;

namespace Apq.ChangeBubbling.Abstractions;

/// <summary>
/// 定义支持“变更冒泡”的节点或容器的通知契约。
/// </summary>
public interface IBubblingChangeNotifier
{
    event EventHandler<BubblingChange>? NodeChanged;
}


