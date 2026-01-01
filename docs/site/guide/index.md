# 简介

Apq.ChangeBubbling 是一个变更冒泡事件库，提供树形数据结构的变更事件自动冒泡、Rx 响应式流、弱引用消息和可插拔调度环境。

## 为什么选择 Apq.ChangeBubbling？

### 🌳 变更事件冒泡

子节点的变更事件自动向上冒泡到父节点，携带完整路径信息：

```csharp
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

root.AttachChild(child);

root.NodeChanged += (sender, change) =>
{
    // 可以追踪变更来源的完整路径
    Console.WriteLine($"路径: {string.Join(".", change.PathSegments)}");
};

child.Add(42);  // 输出: 路径: Child.0
```

### 📡 Rx 响应式流

基于 System.Reactive 的响应式编程支持：

```csharp
// 订阅原始事件流
ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine($"收到变更: {change.PropertyName}"));

// 节流事件流
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change => Console.WriteLine($"节流后: {change.PropertyName}"));

// 缓冲批量事件流
ChangeMessenger.AsBufferedObservable(TimeSpan.FromSeconds(1), 100)
    .Subscribe(changes => Console.WriteLine($"批量收到 {changes.Count} 个变更"));
```

### 💬 弱引用消息

集成 CommunityToolkit.Mvvm 弱引用消息，避免内存泄漏：

- 自动清理失效订阅
- 支持跨组件通信
- 无需手动取消订阅

### ⚡ 可插拔调度环境

支持多种调度模式：

```csharp
// 使用线程池（默认）
ChangeMessenger.RegisterThreadPool("default");

// 使用 UI 线程
ChangeMessenger.RegisterDispatcher("ui");

// 使用专用线程
var disposable = ChangeMessenger.RegisterDedicatedThread("worker", "WorkerThread");

// 发布到指定环境
ChangeMessenger.Publish(change, "ui");
```

### 🎯 事件过滤

内置多种过滤器：

```csharp
// 基于属性的过滤器
var propertyFilter = new PropertyBasedEventFilter(
    allowedProperties: new[] { "Name", "Value" },
    excludedKinds: new[] { NodeChangeKind.CollectionReset }
);

// 基于路径的过滤器
var pathFilter = new PathBasedEventFilter(
    allowedPaths: new[] { "Root.Settings" },
    maxDepth: 3
);

// 基于频率的过滤器（节流）
var frequencyFilter = new FrequencyBasedEventFilter(
    throttleInterval: TimeSpan.FromMilliseconds(100)
);
```

### 📸 快照服务

支持节点树的快照导出与导入：

```csharp
// 导出快照
var snapshot = TreeSnapshotService.Export(rootNode);
var json = SnapshotSerializer.ToJson(snapshot);

// 导入快照
var loadedSnapshot = SnapshotSerializer.FromJson(json);
var restoredNode = TreeSnapshotService.Import(loadedSnapshot);
```

## 快速开始

### 安装

```bash
dotnet add package Apq.ChangeBubbling
```

### 基本用法

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建节点
var root = new ListBubblingNode<string>("Root");

// 订阅变更
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"变更类型: {change.Kind}");
};

// 操作节点
root.Add("Hello");
root.Add("World");
```

## 兼容性

| 框架 | 版本 |
|------|------|
| .NET | 8.0, 10.0 (LTS) |

## 下一步

- [快速开始](/guide/quick-start) - 5 分钟上手教程
- [节点类型](/guide/node-types) - 了解所有节点类型
- [事件冒泡](/guide/event-bubbling) - 深入理解冒泡机制
- [API 参考](/api/) - 完整的 API 文档
