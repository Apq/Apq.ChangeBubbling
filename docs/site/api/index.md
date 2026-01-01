# API 参考

本节包含 Apq.ChangeBubbling 所有公开 API 的详细文档。

## 自动生成的 API 文档

API 文档由代码注释自动生成，按 .NET 版本分类：

- [.NET 10.0](./net10.0/)
- [.NET 8.0](./net8.0/)

## 手动编写的 API 文档

- [节点类型](./nodes) - 节点接口和实现类
- [消息系统](./messaging) - 消息发布和订阅
- [事件过滤](./filtering) - 事件过滤器
- [快照服务](./snapshot) - 快照导出和导入

## 主要命名空间

### Apq.ChangeBubbling.Core

核心接口和基类：
- `IChangeNode` - 节点接口
- `BubblingNodeBase` - 节点基类

### Apq.ChangeBubbling.Abstractions

抽象定义：
- `BubblingChange` - 变更事件结构
- `NodeChangeKind` - 变更类型枚举

### Apq.ChangeBubbling.Nodes

节点实现：
- `ListBubblingNode<T>` - 列表节点
- `DictionaryBubblingNode<TKey, TValue>` - 字典节点

### Apq.ChangeBubbling.Nodes.Concurrent

并发节点：
- `ConcurrentBagBubblingNode<T>` - 线程安全列表节点
- `ConcurrentDictionaryBubblingNode<TKey, TValue>` - 线程安全字典节点

### Apq.ChangeBubbling.Messaging

消息系统：
- `ChangeMessenger` - 消息中心
- `BubblingChangeMessage` - 消息对象

### Apq.ChangeBubbling.Infrastructure.EventFiltering

事件过滤：
- `IChangeEventFilter` - 过滤器接口
- `PropertyNameFilter` - 属性名称过滤器
- `NodeNameFilter` - 节点名称过滤器
- `ChangeKindFilter` - 变更类型过滤器

### Apq.ChangeBubbling.Snapshot

快照服务：
- `TreeSnapshotService` - 树快照服务
- `SnapshotSerializer` - 快照序列化
- `NodeSnapshot` - 节点快照
- `ISnapshotSerializable` - 可序列化接口
