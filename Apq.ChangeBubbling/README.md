# Apq.ChangeBubbling

[![Gitee](https://img.shields.io/badge/Gitee-Apq.ChangeBubbling-red)](https://gitee.com/apq/Apq.ChangeBubbling)
[![Documentation](https://img.shields.io/badge/文档-Vercel-blue)](https://apq-changebubbling.vercel.app/)

变更冒泡事件库，提供树形数据结构的变更事件自动冒泡、Rx 响应式流、弱引用消息和可插拔调度环境。

**仓库地址**：https://gitee.com/apq/Apq.ChangeBubbling

**📖 在线文档**：https://apq-changebubbling.vercel.app/

## 项目结构

```text
Apq.ChangeBubbling/
├── Abstractions/                # 抽象定义
│   ├── BubblingChange.cs        # 变更事件上下文
│   ├── IBubblingChangeNotifier.cs
│   └── NodeChangeKind.cs        # 变更类型枚举
├── Core/                        # 核心实现
│   ├── IChangeNode.cs           # 节点接口
│   ├── ChangeNodeBase.cs        # 节点基类
│   └── WeakEventSubscription.cs # 弱事件订阅
├── Nodes/                       # 节点实现
│   ├── ListBubblingNode.cs      # 列表节点
│   ├── DictionaryBubblingNode.cs# 字典节点
│   └── Concurrent/              # 线程安全节点
├── Messaging/                   # 消息中心
│   ├── ChangeMessenger.cs       # 消息发布中心
│   └── BubblingChangeMessage.cs # 消息包装
├── Collections/                 # 集合适配器
├── Infrastructure/              # 基础设施
│   ├── Dataflow/                # TPL Dataflow 管线
│   ├── EventFiltering/          # 事件过滤器
│   ├── Performance/             # 性能优化
│   └── Nito/                    # Nito.AsyncEx 集成
└── Snapshot/                    # 快照服务
```

## 特性

- **变更事件冒泡**：子节点的变更事件自动向上冒泡到父节点，携带完整路径信息
- **多种消息通道**：支持 Rx Subject 响应式流和 WeakReferenceMessenger 弱引用消息
- **可插拔调度环境**：支持线程池、UI 线程、专用线程、Nito.AsyncEx 等多种调度模式
- **事件过滤**：内置属性过滤、路径过滤、频率节流等多种过滤器
- **背压管线**：基于 TPL Dataflow 的背压处理管线
- **批量操作**：支持批量变更和事件合并，减少高频场景下的事件风暴
- **快照服务**：支持节点树的快照导出与导入
- **线程安全**：提供线程安全的并发集合节点
- **高性能**：使用弱事件订阅、路径缓存、ArrayPool 等优化技术

## 支持的框架

- .NET 8.0
- .NET 10.0

## 安装

```bash
dotnet add package Apq.ChangeBubbling
```

## 快速开始

### 基本用法

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建节点树
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

// 建立父子关系
root.AttachChild(child);

// 订阅变更事件
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"变更: {change.PropertyName}, 类型: {change.Kind}, 路径: {string.Join(".", change.PathSegments)}");
};

// 子节点的变更会自动冒泡到父节点
child.Add(42);
child.Add(100);
```

### 使用 Rx 响应式流

```csharp
using Apq.ChangeBubbling.Messaging;

// 订阅原始事件流
ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine($"收到变更: {change.PropertyName}"));

// 订阅节流事件流
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change => Console.WriteLine($"节流后: {change.PropertyName}"));

// 订阅缓冲批量事件流
ChangeMessenger.AsBufferedObservable(TimeSpan.FromSeconds(1), 100)
    .Subscribe(changes => Console.WriteLine($"批量收到 {changes.Count} 个变更"));
```

### 配置调度环境

```csharp
// 使用线程池（默认）
ChangeMessenger.RegisterThreadPool("default");

// 使用 UI 线程（需在 UI 线程调用）
ChangeMessenger.RegisterDispatcher("ui");

// 使用专用线程
var disposable = ChangeMessenger.RegisterDedicatedThread("worker", "WorkerThread");

// 发布到指定环境
ChangeMessenger.Publish(change, "ui");
```

### 事件过滤

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;

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

// 组合过滤器
var compositeFilter = new CompositeEventFilter(
    new IChangeEventFilter[] { propertyFilter, pathFilter },
    CompositeEventFilter.FilterMode.All
);

// 注册过滤器
ChangeMessenger.RegisterFilter("default", compositeFilter);
```

### 批量操作

```csharp
var node = new ListBubblingNode<int>("Numbers");

// 开始批量操作
node.BeginBatch();
try
{
    for (int i = 0; i < 1000; i++)
    {
        node.Add(i);
    }
}
finally
{
    // 结束批量操作，一次性触发所有事件
    node.EndBatch();
}
```

### 事件合并

```csharp
var node = new ListBubblingNode<int>("Numbers");

// 开始事件合并模式
node.BeginCoalesce();
try
{
    // 多次修改同一属性，只保留最终值
    node.Add(1);
    node.Add(2);
    node.Add(3);
}
finally
{
    // 结束合并，触发合并后的事件
    node.EndCoalesce();
}
```

### 使用背压管线

```csharp
using Apq.ChangeBubbling.Infrastructure.Dataflow;

// 创建背压管线
using var pipeline = new ChangeDataflowPipeline(
    handler: change => ProcessChange(change),
    boundedCapacity: 10000,
    maxDegreeOfParallelism: 4
);

// 发送变更到管线
pipeline.Post(change);

// 完成处理
pipeline.Complete();
```

### 快照导出与导入

```csharp
using Apq.ChangeBubbling.Snapshot;

// 导出快照
var snapshot = TreeSnapshotService.Export(rootNode);
var json = SnapshotSerializer.ToJson(snapshot);

// 导入快照
var loadedSnapshot = SnapshotSerializer.FromJson(json);
var restoredNode = TreeSnapshotService.Import(loadedSnapshot);
```

## 核心类型

### 节点类型

| 类型 | 描述 |
| ---- | ---- |
| `ChangeNodeBase` | 节点基类，提供父子管理与冒泡转译 |
| `ListBubblingNode<T>` | 基于列表的冒泡节点 |
| `DictionaryBubblingNode<TKey, TValue>` | 基于字典的冒泡节点 |
| `ConcurrentBagBubblingNode<T>` | 线程安全的列表冒泡节点 |
| `ConcurrentDictionaryBubblingNode<TKey, TValue>` | 线程安全的字典冒泡节点 |

### 变更类型

| 类型 | 描述 |
| ---- | ---- |
| `PropertyUpdate` | 属性值更新 |
| `CollectionAdd` | 集合添加元素 |
| `CollectionRemove` | 集合移除元素 |
| `CollectionReplace` | 集合替换元素 |
| `CollectionMove` | 集合移动元素 |
| `CollectionReset` | 集合重置 |

### 过滤器类型

| 类型 | 描述 |
| ---- | ---- |
| `PropertyBasedEventFilter` | 基于属性名和变更类型的过滤器 |
| `PathBasedEventFilter` | 基于路径和深度的过滤器 |
| `FrequencyBasedEventFilter` | 基于频率的节流过滤器 |
| `CompositeEventFilter` | 组合多个过滤器 |

## 性能优化

库内置多项性能优化：

- **弱事件订阅**：避免内存泄漏，自动清理失效订阅
- **路径缓存**：缓存常用路径段，减少字符串分配
- **ArrayPool**：长路径使用 ArrayPool 减少 GC 压力
- **快照缓存**：集合快照缓存，避免重复创建
- **无锁快速路径**：批量/合并模式使用 volatile 标志位实现无锁快速检查

## 依赖项

| 包名 | 用途 |
| ---- | ---- |
| [System.Reactive](https://github.com/dotnet/reactive) | Rx 响应式编程 |
| [System.Threading.Tasks.Dataflow](https://docs.microsoft.com/dotnet/standard/parallel-programming/dataflow-task-parallel-library) | TPL Dataflow 背压管线 |
| [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) | 弱引用消息 |
| [Castle.Core](https://github.com/castleproject/Core) | 动态代理 |
| [Nito.AsyncEx](https://github.com/StephenCleary/AsyncEx) | 异步上下文 |
| [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) | 自动织入 INotifyPropertyChanged |

## 许可证

MIT License

## 作者

- 邮箱：amwpfiqvy@163.com
