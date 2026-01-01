# Performance

This document covers performance optimization tips for Apq.ChangeBubbling.

## Batch Operations

```csharp
// Not recommended: triggers 1000 events
for (int i = 0; i < 1000; i++)
{
    users.Add(new User());
}

// Recommended: triggers 1 event
users.BeginBatch();
for (int i = 0; i < 1000; i++)
{
    users.Add(new User());
}
users.EndBatch();
```

## Silent Population

```csharp
// Use for initialization
users.PopulateSilently(initialData);
```

## Event Filtering

```csharp
// Filter at source
ChangeMessenger.RegisterFilter("filter",
    new PropertyNameFilter("Name", "Email"));
```

## Throttling

```csharp
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => HandleChange(c));
```

## Memory Optimization

```csharp
// Enable weak references
ChangeMessenger.EnableWeakMessenger = true;

// Dispose subscriptions when done
subscription.Dispose();
```

## Concurrency

```csharp
// Use concurrent nodes for multi-threading
var logs = new ConcurrentBagBubblingNode<LogEntry>("Logs");

// Use thread pool scheduler
ChangeMessenger.RegisterThreadPool("background");
```

## Performance Monitoring

```csharp
ChangeMessenger.EnableMetrics = true;

var metrics = ChangeMessenger.GetPerformanceMetrics();
Console.WriteLine($"Total: {metrics.TotalEvents}");
Console.WriteLine($"Rate: {metrics.EventsPerSecond}/s");
```

## Benchmark Results

| Operation | Ops/Second |
|-----------|------------|
| Single Add | ~100,000 |
| Batch Add (1000) | ~1,000,000 |
| Silent Populate (1000) | ~5,000,000 |
| Event Publish | ~500,000 |
| Rx Subscribe | ~200,000 |
