# Node Types API

## IChangeNode

Node interface implemented by all node types.

```csharp
public interface IChangeNode
{
    string NodeName { get; }
    IChangeNode? Parent { get; }
    IReadOnlyList<IChangeNode> Children { get; }
    event EventHandler<BubblingChange>? NodeChanged;
    void AttachChild(IChangeNode child);
    void DetachChild(IChangeNode child);
}
```

## ListBubblingNode&lt;T&gt;

List node for ordered collections.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Items` | `IReadOnlyList<T>` | Read-only element list |
| `Count` | `int` | Element count |

### Methods

| Method | Description |
|--------|-------------|
| `Add(T item)` | Add element |
| `Insert(int index, T item)` | Insert at position |
| `RemoveAt(int index)` | Remove at position |
| `Remove(T item)` | Remove element |
| `PopulateSilently(IEnumerable<T> items)` | Silent populate |

## DictionaryBubblingNode&lt;TKey, TValue&gt;

Dictionary node for key-value pairs.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Keys` | `IReadOnlyCollection<TKey>` | Read-only keys |
| `Items` | `IReadOnlyDictionary<TKey, TValue>` | Read-only dictionary |
| `Count` | `int` | Element count |

### Methods

| Method | Description |
|--------|-------------|
| `Put(TKey key, TValue value)` | Add or update |
| `TryGet(TKey key, out TValue value)` | Try get value |
| `Remove(TKey key)` | Remove key-value pair |
| `PopulateSilently(...)` | Silent populate |

## Concurrent Nodes

### ConcurrentBagBubblingNode&lt;T&gt;

Thread-safe list node.

### ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;

Thread-safe dictionary node.
