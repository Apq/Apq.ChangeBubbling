# Messaging

Apq.ChangeBubbling provides a powerful messaging system with weak reference subscriptions, multiple scheduling environments, and performance monitoring.

## ChangeMessenger

`ChangeMessenger` is the core class of the messaging system.

### Basic Configuration

```csharp
using Apq.ChangeBubbling.Messaging;

ChangeMessenger.EnableWeakMessenger = true;
ChangeMessenger.EnableRxStream = true;
ChangeMessenger.EnableMetrics = true;
ChangeMessenger.UseSynchronousPublish = true;
```

## Scheduling Environments

### Dispatcher Scheduler

```csharp
ChangeMessenger.RegisterDispatcher("ui");
ChangeMessenger.Publish(change, "ui");
```

### ThreadPool Scheduler

```csharp
ChangeMessenger.RegisterThreadPool("background");
ChangeMessenger.Publish(change, "background");
```

### Dedicated Thread Scheduler

```csharp
ChangeMessenger.RegisterDedicatedThread("worker", "WorkerThread");
ChangeMessenger.Publish(change, "worker");
```

## Publishing Messages

```csharp
ChangeMessenger.PublishToDefaultEnv(change);
ChangeMessenger.PublishToNamedEnv(change, "ui");
ChangeMessenger.Publish(change, "ui");
```

## Subscribing to Messages

### Using Rx Streams

```csharp
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine(change));

ChangeMessenger.AsObservable("ui")
    .Subscribe(change => UpdateUI(change));

ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change => HandleThrottled(change));
```

## Event Filtering

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;

ChangeMessenger.RegisterFilter("myFilter", new PropertyNameFilter("Name", "Age"));
ChangeMessenger.RemoveFilter("myFilter");
```

## Performance Monitoring

```csharp
ChangeMessenger.EnableMetrics = true;

var metrics = ChangeMessenger.GetPerformanceMetrics();
Console.WriteLine($"Total Events: {metrics.TotalEvents}");
Console.WriteLine($"Events/Second: {metrics.EventsPerSecond}");

var stats = ChangeMessenger.GetEventTypeStatistics();
foreach (var (type, count) in stats)
{
    Console.WriteLine($"{type}: {count}");
}
```
