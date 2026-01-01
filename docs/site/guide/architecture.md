# 架构设计

本文介绍 Apq.ChangeBubbling 的整体架构设计。

## 核心概念

### 节点树

Apq.ChangeBubbling 使用树形结构组织数据：

```
Root
├── Users (ListBubblingNode<User>)
│   ├── User1
│   └── User2
├── Settings (DictionaryBubblingNode<string, object>)
│   ├── Theme
│   └── Language
└── Logs (ConcurrentBagBubblingNode<LogEntry>)
```

### 事件冒泡

子节点的变更事件会自动向上冒泡到父节点，直到根节点。

### 消息系统

全局消息系统负责事件的发布和订阅，支持多种调度环境。

## 模块划分

```
Apq.ChangeBubbling
├── Core                    # 核心接口和基类
│   ├── IChangeNode         # 节点接口
│   └── BubblingNodeBase    # 节点基类
├── Abstractions            # 抽象定义
│   ├── BubblingChange      # 变更事件结构
│   └── NodeChangeKind      # 变更类型枚举
├── Nodes                   # 节点实现
│   ├── ListBubblingNode    # 列表节点
│   ├── DictionaryBubblingNode  # 字典节点
│   └── Concurrent          # 并发节点
├── Messaging               # 消息系统
│   ├── ChangeMessenger     # 消息中心
│   └── BubblingChangeMessage  # 消息对象
├── Infrastructure          # 基础设施
│   ├── EventFiltering      # 事件过滤
│   └── Dataflow            # 背压管线
└── Snapshot                # 快照服务
    ├── TreeSnapshotService # 树快照服务
    └── SnapshotSerializer  # 快照序列化
```

## 设计原则

### 1. 单一职责

每个类只负责一个功能：
- 节点类负责数据管理和事件触发
- 消息系统负责事件分发
- 快照服务负责状态持久化

### 2. 开闭原则

通过接口和抽象类实现扩展：
- `IChangeNode` 接口允许自定义节点
- `IChangeEventFilter` 接口允许自定义过滤器
- `ISnapshotSerializable` 接口允许自定义序列化

### 3. 依赖倒置

高层模块不依赖低层模块：
- 消息系统依赖 `BubblingChange` 结构，而非具体节点类
- 快照服务依赖 `IChangeNode` 接口，而非具体实现

## 线程安全

### 并发节点

`Concurrent` 命名空间下的节点是线程安全的：
- `ConcurrentBagBubblingNode<T>`
- `ConcurrentDictionaryBubblingNode<TKey, TValue>`

### 消息系统

消息系统支持多线程发布和订阅：
- 使用 `ConcurrentDictionary` 管理订阅
- 支持多种调度环境

### 对象池

使用对象池减少 GC 压力：
- `BubblingChangeMessage` 使用对象池
- 自动租用和归还

## 扩展点

### 自定义节点

```csharp
public class CustomNode : BubblingNodeBase
{
    // 自定义实现
}
```

### 自定义过滤器

```csharp
public class CustomFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        // 自定义过滤逻辑
    }
}
```

### 自定义调度器

```csharp
ChangeMessenger.RegisterCustomScheduler("custom", scheduler);
```
