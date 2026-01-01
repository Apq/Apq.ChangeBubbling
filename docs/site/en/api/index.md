# API Overview

This section provides the complete API reference for Apq.ChangeBubbling.

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

- [ListBubblingNode&lt;T&gt;](/en/api/nodes#listbubblingnode) - List node
- [DictionaryBubblingNode&lt;TKey, TValue&gt;](/en/api/nodes#dictionarybubblingnode) - Dictionary node
- [ConcurrentBagBubblingNode&lt;T&gt;](/en/api/nodes#concurrentbagbubblingnode) - Thread-safe list node
- [ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;](/en/api/nodes#concurrentdictionarybubblingnode) - Thread-safe dictionary node

### Messaging

- [ChangeMessenger](/en/api/messaging#changemessenger) - Message publishing center
- [BubblingChangeMessage](/en/api/messaging#bubblingchangemessage) - Message wrapper class

### Event Filtering

- [PropertyBasedEventFilter](/en/api/filtering#propertybasedeventfilter) - Property filter
- [PathBasedEventFilter](/en/api/filtering#pathbasedeventfilter) - Path filter
- [FrequencyBasedEventFilter](/en/api/filtering#frequencybasedeventfilter) - Frequency filter
- [CompositeEventFilter](/en/api/filtering#compositeeventfilter) - Composite filter

## Change Kind Enum

```csharp
public enum NodeChangeKind
{
    PropertyUpdate,      // Property value update
    CollectionAdd,       // Collection add element
    CollectionRemove,    // Collection remove element
    CollectionReplace,   // Collection replace element
    CollectionMove,      // Collection move element
    CollectionReset      // Collection reset
}
```
