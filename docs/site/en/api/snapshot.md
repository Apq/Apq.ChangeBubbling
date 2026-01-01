# Snapshot API

## TreeSnapshotService

Tree snapshot service for exporting and importing node tree state.

### Static Methods

```csharp
static NodeSnapshot Export(IChangeNode root);
static IChangeNode Import(NodeSnapshot snapshot);
static void ImportInto(IChangeNode target, NodeSnapshot snapshot);
```

## NodeSnapshot

Node snapshot class.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `NodeName` | `string` | Node name |
| `NodeType` | `string` | Node type |
| `Properties` | `Dictionary<string, object>` | Node properties |
| `Children` | `List<NodeSnapshot>` | Child snapshots |

## SnapshotSerializer

Snapshot serializer.

### Static Methods

```csharp
static string ToJson(NodeSnapshot snapshot);
static NodeSnapshot FromJson(string json);
static byte[] ToBinary(NodeSnapshot snapshot);
static NodeSnapshot FromBinary(byte[] data);
```

## ISnapshotSerializable

Interface for custom serialization.

```csharp
public interface ISnapshotSerializable
{
    Dictionary<string, object> GetSnapshotProperties();
    void ApplySnapshotProperties(Dictionary<string, object> properties);
}
```

## Example

```csharp
// Export
var snapshot = TreeSnapshotService.Export(root);
var json = SnapshotSerializer.ToJson(snapshot);

// Import
var loaded = SnapshotSerializer.FromJson(json);
var newRoot = TreeSnapshotService.Import(loaded);
```
