# 消息系统

Apq.ChangeBubbling 提供了强大的消息系统，支持弱引用订阅、多调度环境和性能监控。

## ChangeMessenger

`ChangeMessenger` 是消息系统的核心类，提供全局消息发布和订阅功能。

### 基本配置

```csharp
using Apq.ChangeBubbling.Messaging;

// 启用弱引用消息（避免内存泄漏）
ChangeMessenger.EnableWeakMessenger = true;

// 启用 Rx 流
ChangeMessenger.EnableRxStream = true;

// 启用性能指标
ChangeMessenger.EnableMetrics = true;

// 使用同步发布（默认异步）
ChangeMessenger.UseSynchronousPublish = true;
```

## 调度环境

消息系统支持多种调度环境，可以控制事件处理的线程：

### 默认调度器

```csharp
// 注册 UI 调度器（WPF/WinForms）
ChangeMessenger.RegisterDispatcher("ui");

// 发布到 UI 线程
ChangeMessenger.Publish(change, "ui");
```

### 线程池调度器

```csharp
// 注册线程池调度器
ChangeMessenger.RegisterThreadPool("background");

// 发布到线程池
ChangeMessenger.Publish(change, "background");
```

### 专用线程调度器

```csharp
// 注册专用线程
ChangeMessenger.RegisterDedicatedThread("worker", "WorkerThread");

// 发布到专用线程
ChangeMessenger.Publish(change, "worker");
```

### Nito.AsyncEx 调度器

```csharp
// 注册 Nito.AsyncEx 上下文
ChangeMessenger.RegisterNitoAsyncContext(
    "async",
    async () => await Task.CompletedTask,
    action => Task.Run(action)
);
```

## 发布消息

```csharp
// 发布到默认环境
ChangeMessenger.PublishToDefaultEnv(change);

// 发布到指定环境
ChangeMessenger.PublishToNamedEnv(change, "ui");

// 简写形式
ChangeMessenger.Publish(change, "ui");
```

## 订阅消息

### 使用 Rx 流

```csharp
using System.Reactive.Linq;

// 订阅所有消息
ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine(change));

// 订阅指定环境的消息
ChangeMessenger.AsObservable("ui")
    .Subscribe(change => UpdateUI(change));

// 带节流的订阅
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change => HandleThrottled(change));
```

## 事件过滤

可以注册过滤器来筛选事件：

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;

// 注册过滤器
ChangeMessenger.RegisterFilter("myFilter", new PropertyNameFilter("Name", "Age"));

// 移除过滤器
ChangeMessenger.RemoveFilter("myFilter");
```

## 性能监控

```csharp
// 启用性能指标
ChangeMessenger.EnableMetrics = true;

// 获取性能指标
var metrics = ChangeMessenger.GetPerformanceMetrics();
Console.WriteLine($"Total Events: {metrics.TotalEvents}");
Console.WriteLine($"Events/Second: {metrics.EventsPerSecond}");

// 获取事件类型统计
var stats = ChangeMessenger.GetEventTypeStatistics();
foreach (var (type, count) in stats)
{
    Console.WriteLine($"{type}: {count}");
}
```

## 消息池

为了减少 GC 压力，消息系统使用对象池：

```csharp
// 从池中租用消息对象
var message = BubblingChangeMessage.Rent(change);

try
{
    // 使用消息
    ProcessMessage(message);
}
finally
{
    // 归还到池中
    message.Return();
}
```
