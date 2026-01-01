# Node Types

Apq.ChangeBubbling provides various node types for building tree-structured data.

## Base Node

### BubblingNodeBase

Abstract base class for all nodes:

```csharp
public abstract class BubblingNodeBase : IChangeNode
{
    public string NodeName { get; }
    public IChangeNode? Parent { get; }
    public IReadOnlyList<IChangeNode> Children { get; }
}
```

## Collection Nodes

### ListBubblingNode&lt;T&gt;

List node for ordered collections:

```csharp
var users = new ListBubblingNode<User>("Users");

users.Add(new User { Name = "Alice" });
users.Insert(0, new User { Name = "Bob" });
users.RemoveAt(0);
```

### DictionaryBubblingNode&lt;TKey, TValue&gt;

Dictionary node for key-value pairs:

```csharp
var settings = new DictionaryBubblingNode<string, object>("Settings");

settings.Put("Theme", "Dark");
if (settings.TryGet("Theme", out var theme))
{
    Console.WriteLine($"Theme: {theme}");
}
settings.Remove("Theme");
```

## Concurrent Nodes

### ConcurrentBagBubblingNode&lt;T&gt;

Thread-safe list node:

```csharp
var logs = new ConcurrentBagBubblingNode<LogEntry>("Logs");

Parallel.For(0, 100, i =>
{
    logs.Add(new LogEntry { Message = $"Log {i}" });
});
```

### ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;

Thread-safe dictionary node:

```csharp
var cache = new ConcurrentDictionaryBubblingNode<string, object>("Cache");

Parallel.For(0, 100, i =>
{
    cache.Put($"Key{i}", $"Value{i}");
});
```

## Building Node Trees

```csharp
var root = new ListBubblingNode<object>("Root");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);

root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
};
```

## Silent Operations

Use `PopulateSilently` to populate data without triggering events:

```csharp
users.PopulateSilently(new[]
{
    new User { Name = "Alice" },
    new User { Name = "Bob" }
});
```
