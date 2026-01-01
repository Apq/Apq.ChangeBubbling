# Messaging API

## ChangeMessenger

Global messaging center for event publishing and subscription.

### Static Properties

| Property | Type | Description |
|----------|------|-------------|
| `EnableWeakMessenger` | `bool` | Enable weak reference messaging |
| `EnableRxStream` | `bool` | Enable Rx streams |
| `EnableMetrics` | `bool` | Enable performance metrics |
| `UseSynchronousPublish` | `bool` | Use synchronous publishing |

### Scheduler Registration

```csharp
static void RegisterDispatcher(string envName);
static void RegisterThreadPool(string envName);
static void RegisterDedicatedThread(string envName, string threadName);
```

### Publishing

```csharp
static void Publish(BubblingChange change, string envName = null);
static void PublishToDefaultEnv(BubblingChange change);
static void PublishToNamedEnv(BubblingChange change, string envName);
```

### Subscription

```csharp
static IObservable<BubblingChange> AsObservable(string envName = null);
static IObservable<BubblingChange> AsThrottledObservable(TimeSpan? throttleTime = null, string envName = null);
```

### Filter Management

```csharp
static void RegisterFilter(string name, IChangeEventFilter filter);
static void RemoveFilter(string name);
```

### Performance Monitoring

```csharp
static PerformanceMetrics GetPerformanceMetrics();
static Dictionary<NodeChangeKind, long> GetEventTypeStatistics();
```

## BubblingChange

Change event structure.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Kind` | `NodeChangeKind` | Change type |
| `NodeName` | `string` | Node name |
| `PropertyName` | `string?` | Property name |
| `Path` | `string` | Full path |
| `OldValue` | `object?` | Old value |
| `NewValue` | `object?` | New value |
| `Timestamp` | `DateTime` | Timestamp |

## NodeChangeKind

Change type enumeration.

| Value | Description |
|-------|-------------|
| `PropertyChanged` | Property value changed |
| `CollectionAdd` | Element added |
| `CollectionRemove` | Element removed |
| `CollectionReplace` | Element replaced |
| `CollectionClear` | Collection cleared |
| `ChildAttached` | Child attached |
| `ChildDetached` | Child detached |
