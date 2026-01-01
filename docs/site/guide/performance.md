# 性能优化

本文介绍 Apq.ChangeBubbling 的性能优化技巧。

## 批量操作

### 使用批量操作减少事件

```csharp
// 不推荐：触发 1000 次事件
for (int i = 0; i < 1000; i++)
{
    users.Add(new User());
}

// 推荐：只触发 1 次事件
users.BeginBatch();
for (int i = 0; i < 1000; i++)
{
    users.Add(new User());
}
users.EndBatch();
```

### 使用静默填充初始化

```csharp
// 初始化数据时使用静默填充
users.PopulateSilently(initialData);
```

## 事件过滤

### 在源头过滤

使用过滤器在发布前过滤事件：

```csharp
// 只处理特定属性的变更
ChangeMessenger.RegisterFilter("filter",
    new PropertyNameFilter("Name", "Email"));
```

### 使用节流

```csharp
// 限制事件频率
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => HandleChange(c));
```

## 内存优化

### 使用弱引用消息

```csharp
// 启用弱引用，避免内存泄漏
ChangeMessenger.EnableWeakMessenger = true;
```

### 及时取消订阅

```csharp
var subscription = ChangeMessenger.AsObservable()
    .Subscribe(c => HandleChange(c));

// 不再需要时取消订阅
subscription.Dispose();
```

### 使用对象池

消息系统自动使用对象池，无需手动管理。

## 并发优化

### 使用并发节点

```csharp
// 多线程场景使用并发节点
var logs = new ConcurrentBagBubblingNode<LogEntry>("Logs");
```

### 使用线程池调度

```csharp
// 注册线程池调度器
ChangeMessenger.RegisterThreadPool("background");

// 发布到线程池处理
ChangeMessenger.Publish(change, "background");
```

### 使用背压管线

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: Environment.ProcessorCount,
    boundedCapacity: 10000
);
```

## 性能监控

### 启用性能指标

```csharp
ChangeMessenger.EnableMetrics = true;

// 获取性能指标
var metrics = ChangeMessenger.GetPerformanceMetrics();
Console.WriteLine($"Total: {metrics.TotalEvents}");
Console.WriteLine($"Rate: {metrics.EventsPerSecond}/s");
```

### 事件类型统计

```csharp
var stats = ChangeMessenger.GetEventTypeStatistics();
foreach (var (type, count) in stats)
{
    Console.WriteLine($"{type}: {count}");
}
```

## 基准测试结果

| 操作 | 每秒操作数 |
|------|-----------|
| 单个添加 | ~100,000 |
| 批量添加 (1000) | ~1,000,000 |
| 静默填充 (1000) | ~5,000,000 |
| 事件发布 | ~500,000 |
| Rx 订阅处理 | ~200,000 |

## 最佳实践

1. **初始化使用静默填充**
2. **批量更新使用 BeginBatch/EndBatch**
3. **启用弱引用消息**
4. **使用事件过滤减少不必要的处理**
5. **多线程场景使用并发节点**
6. **高吞吐场景使用背压管线**
7. **定期监控性能指标**
