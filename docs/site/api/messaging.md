# 消息系统 API

## ChangeMessenger

全局消息中心，负责事件的发布和订阅。

### 静态属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `EnableWeakMessenger` | `bool` | 启用弱引用消息 |
| `EnableRxStream` | `bool` | 启用 Rx 流 |
| `EnableMetrics` | `bool` | 启用性能指标 |
| `UseSynchronousPublish` | `bool` | 使用同步发布 |

### 调度器注册

```csharp
// 注册 UI 调度器
static void RegisterDispatcher(string envName);

// 注册线程池调度器
static void RegisterThreadPool(string envName);

// 注册专用线程调度器
static void RegisterDedicatedThread(string envName, string threadName);

// 注册 Nito.AsyncEx 调度器
static void RegisterNitoAsyncContext(
    string envName,
    Func<Task> runAsync,
    Func<Action, Task> postAsync);
```

### 发布方法

```csharp
// 发布到指定环境
static void Publish(BubblingChange change, string envName = null);

// 发布到默认环境
static void PublishToDefaultEnv(BubblingChange change);

// 发布到命名环境
static void PublishToNamedEnv(BubblingChange change, string envName);
```

### 订阅方法

```csharp
// 获取 Rx 流
static IObservable<BubblingChange> AsObservable(string envName = null);

// 获取带节流的 Rx 流
static IObservable<BubblingChange> AsThrottledObservable(
    TimeSpan? throttleTime = null,
    string envName = null);
```

### 过滤器管理

```csharp
// 注册过滤器
static void RegisterFilter(string name, IChangeEventFilter filter);

// 移除过滤器
static void RemoveFilter(string name);
```

### 性能监控

```csharp
// 获取性能指标
static PerformanceMetrics GetPerformanceMetrics();

// 获取事件类型统计
static Dictionary<NodeChangeKind, long> GetEventTypeStatistics();
```

## BubblingChangeMessage

消息对象，使用对象池管理。

### 静态方法

```csharp
// 从池中租用
static BubblingChangeMessage Rent(BubblingChange change);
```

### 实例方法

```csharp
// 归还到池中
void Return();

// 设置值
void SetValue(BubblingChange change);
```

## BubblingChange

变更事件结构。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Kind` | `NodeChangeKind` | 变更类型 |
| `NodeName` | `string` | 节点名称 |
| `PropertyName` | `string?` | 属性名称 |
| `Path` | `string` | 完整路径 |
| `OldValue` | `object?` | 旧值 |
| `NewValue` | `object?` | 新值 |
| `Timestamp` | `DateTime` | 时间戳 |

## NodeChangeKind

变更类型枚举。

| 值 | 说明 |
|------|------|
| `PropertyChanged` | 属性值变更 |
| `CollectionAdd` | 集合添加元素 |
| `CollectionRemove` | 集合移除元素 |
| `CollectionReplace` | 集合替换元素 |
| `CollectionClear` | 集合清空 |
| `ChildAttached` | 子节点附加 |
| `ChildDetached` | 子节点分离 |
