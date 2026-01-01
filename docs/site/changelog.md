# 更新日志

## v1.0.1

### 改进

- 新增 VitePress 文档站点，支持中英文双语
- 目标框架改为 .NET 8.0 和 .NET 10.0

## v1.0.0

### 新功能

- 变更冒泡机制
  - 子节点变更自动冒泡到父节点
  - 携带完整路径信息
  - 支持多级嵌套

- 节点类型
  - `ListBubblingNode<T>` - 列表节点
  - `DictionaryBubblingNode<TKey, TValue>` - 字典节点
  - `ConcurrentBagBubblingNode<T>` - 线程安全列表节点
  - `ConcurrentDictionaryBubblingNode<TKey, TValue>` - 线程安全字典节点

- Rx 响应式流
  - `AsObservable()` - 原始事件流
  - `AsThrottledObservable()` - 节流事件流
  - `AsBufferedObservable()` - 缓冲批量事件流

- 消息系统
  - 弱引用消息，避免内存泄漏
  - 对象池，零 GC 分配
  - 可插拔调度环境

- 事件过滤
  - `PropertyBasedEventFilter` - 属性过滤器
  - `PathBasedEventFilter` - 路径过滤器
  - `FrequencyBasedEventFilter` - 频率过滤器
  - `CompositeEventFilter` - 组合过滤器

- 批量操作
  - `BeginBatch()`/`EndBatch()` - 批量收集事件
  - `BeginCoalesce()`/`EndCoalesce()` - 事件合并

- 背压管线
  - 基于 TPL Dataflow
  - 可配置容量和并行度

- 快照服务
  - 树形快照导出与导入
  - JSON 序列化支持

### 支持的框架

- .NET 8.0
- .NET 10.0
