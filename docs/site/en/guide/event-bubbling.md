# Event Bubbling

Event bubbling is the core mechanism of Apq.ChangeBubbling, allowing child node changes to propagate up to parent nodes.

## Bubbling Mechanism

When a child node changes, the event automatically bubbles up to all ancestor nodes:

```
Root (receives all events)
├── Users (receives Users and its children's events)
│   ├── User1
│   └── User2
└── Settings (receives Settings and its children's events)
    ├── Theme
    └── Language
```

## Change Types

`NodeChangeKind` enum defines all change types:

| Type | Description |
|------|-------------|
| `PropertyChanged` | Property value changed |
| `CollectionAdd` | Element added to collection |
| `CollectionRemove` | Element removed from collection |
| `CollectionReplace` | Element replaced in collection |
| `CollectionClear` | Collection cleared |
| `ChildAttached` | Child node attached |
| `ChildDetached` | Child node detached |

## Subscribing to Events

### Direct Subscription

```csharp
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
    Console.WriteLine($"  Path: {change.Path}");
    Console.WriteLine($"  OldValue: {change.OldValue}");
    Console.WriteLine($"  NewValue: {change.NewValue}");
};
```

### Using Rx Streams

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));
```

## BubblingChange Structure

```csharp
public readonly struct BubblingChange
{
    public NodeChangeKind Kind { get; }
    public string NodeName { get; }
    public string? PropertyName { get; }
    public string Path { get; }
    public object? OldValue { get; }
    public object? NewValue { get; }
    public DateTime Timestamp { get; }
}
```

## Path Tracking

Each change event includes full path information:

```csharp
// List nodes use index paths
users.Add(new User()); // Path: Root/Users/[0]

// Dictionary nodes use key paths
settings.Put("Theme", "Dark"); // Path: Root/Settings/Theme
```
