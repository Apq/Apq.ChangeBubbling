# API Reference

This section provides the complete API reference for Apq.ChangeBubbling.

## DocFX API Documentation

For detailed API documentation generated from code comments, see [DocFX API Docs](/api-reference/index.html).

## Manual API Docs

- [Node Types](./nodes) - Node interfaces and implementations
- [Messaging](./messaging) - Message publishing and subscription
- [Filtering](./filtering) - Event filters
- [Snapshot](./snapshot) - Snapshot export and import

## Namespaces

| Namespace | Description |
|-----------|-------------|
| `Apq.ChangeBubbling.Abstractions` | Abstract definitions, interfaces, enums |
| `Apq.ChangeBubbling.Core` | Core implementation, node base class |
| `Apq.ChangeBubbling.Nodes` | Node implementation classes |
| `Apq.ChangeBubbling.Messaging` | Message center, Rx streams |
| `Apq.ChangeBubbling.Infrastructure.EventFiltering` | Event filters |
| `Apq.ChangeBubbling.Infrastructure.Dataflow` | TPL Dataflow pipeline |
| `Apq.ChangeBubbling.Snapshot` | Snapshot service |

## Core Types

### Node Types

- `ListBubblingNode<T>` - List node
- `DictionaryBubblingNode<TKey, TValue>` - Dictionary node
- `ConcurrentBagBubblingNode<T>` - Thread-safe list node
- `ConcurrentDictionaryBubblingNode<TKey, TValue>` - Thread-safe dictionary node

### Messaging

- `ChangeMessenger` - Message publishing center
- `BubblingChangeMessage` - Message wrapper class

### Event Filtering

- `IChangeEventFilter` - Filter interface
- `PropertyNameFilter` - Property name filter
- `NodeNameFilter` - Node name filter
- `ChangeKindFilter` - Change kind filter

## Change Kind Enum

```csharp
public enum NodeChangeKind
{
    PropertyChanged,     // Property value changed
    CollectionAdd,       // Collection add element
    CollectionRemove,    // Collection remove element
    CollectionReplace,   // Collection replace element
    CollectionClear,     // Collection cleared
    ChildAttached,       // Child node attached
    ChildDetached        // Child node detached
}
```
