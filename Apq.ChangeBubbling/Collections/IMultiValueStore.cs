using System;
using System.Collections;
using System.Collections.Generic;
using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Collections;

/// <summary>
/// 统一的多值存储抽象，屏蔽 List/Dictionary 等差异，同时提供冒泡事件。
/// Castle 动态代理的通用适配器将实现该接口。
/// </summary>
public interface IMultiValueStore : IBubblingChangeNotifier, IEnumerable
{
    /// <summary>
    /// 容器名称，作为变更的 PropertyName 以及路径段。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 元素数量。
    /// </summary>
    int Count { get; }

    /// <summary>
    /// 获取元素的枚举序列。
    /// </summary>
    IEnumerable<object?> Items { get; }
}


