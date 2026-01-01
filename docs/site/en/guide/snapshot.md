# Snapshot Service

Apq.ChangeBubbling provides snapshot services for exporting and importing node tree state.

## TreeSnapshotService

### Export Snapshot

```csharp
using Apq.ChangeBubbling.Snapshot;

var snapshot = TreeSnapshotService.Export(root);
```

### Import Snapshot

```csharp
var newRoot = TreeSnapshotService.Import(snapshot);

// Or import into existing node
TreeSnapshotService.ImportInto(existingNode, snapshot);
```

## Serialization

### JSON

```csharp
var json = SnapshotSerializer.ToJson(snapshot);
File.WriteAllText("snapshot.json", json);

var loaded = SnapshotSerializer.FromJson(json);
```

### Binary

```csharp
var bytes = SnapshotSerializer.ToBinary(snapshot);
File.WriteAllBytes("snapshot.bin", bytes);

var loaded = SnapshotSerializer.FromBinary(bytes);
```

## Custom Serialization

```csharp
public class CustomNode : BubblingNodeBase, ISnapshotSerializable
{
    public string CustomProperty { get; set; }

    public Dictionary<string, object> GetSnapshotProperties()
    {
        return new Dictionary<string, object>
        {
            ["CustomProperty"] = CustomProperty
        };
    }

    public void ApplySnapshotProperties(Dictionary<string, object> properties)
    {
        if (properties.TryGetValue("CustomProperty", out var value))
        {
            CustomProperty = value?.ToString();
        }
    }
}
```

## Best Practices

1. Save snapshots regularly for data recovery
2. Use incremental snapshots to reduce storage
3. Compress large snapshots with GZip
4. Verify snapshot integrity with checksums
