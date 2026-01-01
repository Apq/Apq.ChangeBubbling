# API 概述

本节提供 Apq.ChangeBubbling 的完整 API 参考文档。

## 命名空间

| 命名空间 | 说明 |
|----------|------|
| `Apq.ChangeBubbling.Abstractions` | 抽象定义、接口、枚举 |
| `Apq.ChangeBubbling.Core` | 核心实现、节点基类 |
| `Apq.ChangeBubbling.Nodes` | 节点实现类 |
| `Apq.ChangeBubbling.Messaging` | 消息中心、Rx 流 |
| `Apq.ChangeBubbling.Infrastructure.EventFiltering` | 事件过滤器 |
| `Apq.ChangeBubbling.Infrastructure.Dataflow` | TPL Dataflow 管线 |
| `Apq.ChangeBubbling.Snapshot` | 快照服务 |

## 核心类型

### 节点类型

- [ListBubblingNode&lt;T&gt;](/api/nodes#listbubblingnode) - 列表节点
- [DictionaryBubblingNode&lt;TKey, TValue&gt;](/api/nodes#dictionarybubblingnode) - 字典节点
- [ConcurrentBagBubblingNode&lt;T&gt;](/api/nodes#concurrentbagbubblingnode) - 线程安全列表节点
- [ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;](/api/nodes#concurrentdictionarybubblingnode) - 线程安全字典节点

### 消息系统

- [ChangeMessenger](/api/messaging#changemessenger) - 消息发布中心
- [BubblingChangeMessage](/api/messaging#bubblingchangemessage) - 消息包装类

### 事件过滤

- [PropertyBasedEventFilter](/api/filtering#propertybasedeventfilter) - 属性过滤器
- [PathBasedEventFilter](/api/filtering#pathbasedeventfilter) - 路径过滤器
- [FrequencyBasedEventFilter](/api/filtering#frequencybasedeventfilter) - 频率过滤器
- [CompositeEventFilter](/api/filtering#compositeeventfilter) - 组合过滤器

### 快照服务

- [TreeSnapshotService](/api/snapshot#treesnapshotservice) - 树形快照服务
- [SnapshotSerializer](/api/snapshot#snapshotserializer) - 快照序列化器

## 变更类型枚举

```csharp
public enum NodeChangeKind
{
    PropertyUpdate,      // 属性值更新
    CollectionAdd,       // 集合添加元素
    CollectionRemove,    // 集合移除元素
    CollectionReplace,   // 集合替换元素
    CollectionMove,      // 集合移动元素
    CollectionReset      // 集合重置
}
```

## BubblingChange 结构体

```csharp
public readonly struct BubblingChange
{
    public string NodeName { get; }
    public string PropertyName { get; }
    public NodeChangeKind Kind { get; }
    public object? OldValue { get; }
    public object? NewValue { get; }
    public IReadOnlyList<string> PathSegments { get; }
}
```
