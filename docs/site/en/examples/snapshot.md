# Snapshot Examples

This section shows how to use snapshot services.

## Export Snapshot

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Snapshot;

var root = new ListBubblingNode<object>("Root");
// ... build tree ...

var snapshot = TreeSnapshotService.Export(root);
```

## Serialize Snapshot

```csharp
var json = SnapshotSerializer.ToJson(snapshot);
File.WriteAllText("snapshot.json", json);
```

## Restore from Snapshot

```csharp
var json = File.ReadAllText("snapshot.json");
var loadedSnapshot = SnapshotSerializer.FromJson(json);
var restoredRoot = TreeSnapshotService.Import(loadedSnapshot);
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

## Complete Example

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Snapshot;

public class Program
{
    public static void Main()
    {
        // Create and populate tree
        var root = new ListBubblingNode<object>("Root");
        var users = new ListBubblingNode<User>("Users");
        root.AttachChild(users);
        users.Add(new User { Name = "Alice" });

        // Export snapshot
        var snapshot = TreeSnapshotService.Export(root);
        var json = SnapshotSerializer.ToJson(snapshot);
        File.WriteAllText("snapshot.json", json);
        Console.WriteLine("Snapshot saved");

        // Restore
        var loadedJson = File.ReadAllText("snapshot.json");
        var loadedSnapshot = SnapshotSerializer.FromJson(loadedJson);
        var restoredRoot = TreeSnapshotService.Import(loadedSnapshot);
        Console.WriteLine("Tree restored");
    }
}
```
