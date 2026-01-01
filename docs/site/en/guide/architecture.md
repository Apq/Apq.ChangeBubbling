# Architecture

This document describes the overall architecture of Apq.ChangeBubbling.

## Core Concepts

### Node Tree

Apq.ChangeBubbling uses a tree structure to organize data:

```
Root
├── Users (ListBubblingNode<User>)
├── Settings (DictionaryBubblingNode<string, object>)
└── Logs (ConcurrentBagBubblingNode<LogEntry>)
```

### Event Bubbling

Child node changes automatically bubble up to parent nodes.

### Messaging System

Global messaging system handles event publishing and subscription.

## Module Structure

```
Apq.ChangeBubbling
├── Core                    # Core interfaces and base classes
├── Abstractions            # Abstract definitions
├── Nodes                   # Node implementations
│   └── Concurrent          # Concurrent nodes
├── Messaging               # Messaging system
├── Infrastructure          # Infrastructure
│   ├── EventFiltering      # Event filtering
│   └── Dataflow            # Backpressure pipeline
└── Snapshot                # Snapshot service
```

## Design Principles

1. **Single Responsibility**: Each class handles one function
2. **Open/Closed**: Extensible through interfaces
3. **Dependency Inversion**: High-level modules don't depend on low-level modules

## Thread Safety

- Concurrent nodes are thread-safe
- Messaging system supports multi-threaded publishing
- Object pools reduce GC pressure

## Extension Points

- Custom nodes by extending `BubblingNodeBase`
- Custom filters by implementing `IChangeEventFilter`
- Custom serialization by implementing `ISnapshotSerializable`
