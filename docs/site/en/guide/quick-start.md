# Quick Start

Get started with Apq.ChangeBubbling in minutes.

## Installation

```bash
dotnet add package Apq.ChangeBubbling
```

## Create Node Tree

```csharp
using Apq.ChangeBubbling.Nodes;

// Create root node
var root = new ListBubblingNode<object>("Root");

// Create child nodes
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

// Attach children
root.AttachChild(users);
root.AttachChild(settings);
```

## Subscribe to Changes

```csharp
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
};
```

## Operate Data

```csharp
// Add to list
users.Add(new User { Name = "Alice" });

// Add to dictionary
settings.Put("Theme", "Dark");
```

## Use Rx Streams

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.EnableRxStream = true;

ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));
```

## Next Steps

- [Node Types](/en/guide/node-types) - Learn about different node types
- [Event Bubbling](/en/guide/event-bubbling) - Understand the bubbling mechanism
- [Messaging](/en/guide/messaging) - Explore the messaging system
