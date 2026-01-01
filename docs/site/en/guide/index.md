# Introduction

Apq.ChangeBubbling is a change bubbling event library that provides automatic change event bubbling for tree-structured data, Rx reactive streams, weak reference messaging, and pluggable scheduling environments.

## Why Apq.ChangeBubbling?

### 🌳 Change Event Bubbling

Child node changes automatically bubble up to parent nodes with full path information:

```csharp
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

root.AttachChild(child);

root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"Path: {string.Join(".", change.PathSegments)}");
};

child.Add(42);  // Output: Path: Child.0
```

### 📡 Rx Reactive Streams

System.Reactive based reactive programming support:

```csharp
// Subscribe to raw event stream
ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine($"Received: {change.PropertyName}"));

// Throttled event stream
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change => Console.WriteLine($"Throttled: {change.PropertyName}"));
```

### 💬 Weak Reference Messaging

CommunityToolkit.Mvvm integration for memory-safe messaging:

- Automatic cleanup of invalid subscriptions
- Cross-component communication
- No manual unsubscription needed

### ⚡ Pluggable Scheduling

Multiple scheduling modes:

```csharp
// Thread pool (default)
ChangeMessenger.RegisterThreadPool("default");

// UI thread
ChangeMessenger.RegisterDispatcher("ui");

// Dedicated thread
var disposable = ChangeMessenger.RegisterDedicatedThread("worker", "WorkerThread");
```

## Quick Start

### Installation

```bash
dotnet add package Apq.ChangeBubbling
```

### Basic Usage

```csharp
using Apq.ChangeBubbling.Nodes;

var root = new ListBubblingNode<string>("Root");

root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"Change type: {change.Kind}");
};

root.Add("Hello");
root.Add("World");
```

## Compatibility

| Framework | Version |
|-----------|---------|
| .NET | 8.0, 10.0 (LTS) |

## Next Steps

- [Quick Start](/en/guide/quick-start) - 5-minute tutorial
- [Node Types](/en/guide/node-types) - Learn about node types
- [Event Bubbling](/en/guide/event-bubbling) - Understand bubbling mechanism
- [API Reference](/en/api/) - Complete API documentation
