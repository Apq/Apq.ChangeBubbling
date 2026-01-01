# 快照导出示例

本节展示如何使用快照服务导出和导入节点树状态。

## 导出快照

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Snapshot;

// 创建节点树
var root = new ListBubblingNode<object>("Root");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);

// 添加数据
users.Add(new User { Name = "Alice", Email = "alice@example.com" });
users.Add(new User { Name = "Bob", Email = "bob@example.com" });
settings.Put("Theme", "Dark");
settings.Put("Language", "zh-CN");

// 导出快照
var snapshot = TreeSnapshotService.Export(root);
```

## 序列化快照

```csharp
// 序列化为 JSON
var json = SnapshotSerializer.ToJson(snapshot);
Console.WriteLine(json);

// 保存到文件
File.WriteAllText("snapshot.json", json);
```

## 从快照恢复

```csharp
// 从文件加载
var json = File.ReadAllText("snapshot.json");

// 反序列化
var loadedSnapshot = SnapshotSerializer.FromJson(json);

// 创建新节点树
var restoredRoot = TreeSnapshotService.Import(loadedSnapshot);

// 或导入到现有节点
TreeSnapshotService.ImportInto(existingRoot, loadedSnapshot);
```

## 自定义序列化

```csharp
public class CustomNode : BubblingNodeBase, ISnapshotSerializable
{
    public string CustomProperty { get; set; }
    public DateTime CreatedAt { get; set; }

    public Dictionary<string, object> GetSnapshotProperties()
    {
        return new Dictionary<string, object>
        {
            ["CustomProperty"] = CustomProperty,
            ["CreatedAt"] = CreatedAt.ToString("O")
        };
    }

    public void ApplySnapshotProperties(Dictionary<string, object> properties)
    {
        if (properties.TryGetValue("CustomProperty", out var prop))
        {
            CustomProperty = prop?.ToString();
        }
        if (properties.TryGetValue("CreatedAt", out var created))
        {
            CreatedAt = DateTime.Parse(created?.ToString());
        }
    }
}
```

## 完整示例

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Snapshot;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Program
{
    public static void Main()
    {
        // 创建并填充节点树
        var root = CreateAndPopulateTree();

        // 导出快照
        var snapshot = TreeSnapshotService.Export(root);
        Console.WriteLine("Snapshot exported");

        // 序列化
        var json = SnapshotSerializer.ToJson(snapshot);
        File.WriteAllText("snapshot.json", json);
        Console.WriteLine($"Saved to snapshot.json ({json.Length} bytes)");

        // 模拟程序重启...
        Console.WriteLine("\n--- Simulating restart ---\n");

        // 从文件恢复
        var loadedJson = File.ReadAllText("snapshot.json");
        var loadedSnapshot = SnapshotSerializer.FromJson(loadedJson);

        // 创建新节点树
        var restoredRoot = TreeSnapshotService.Import(loadedSnapshot);
        Console.WriteLine("Tree restored from snapshot");

        // 验证数据
        PrintTree(restoredRoot);
    }

    static ListBubblingNode<object> CreateAndPopulateTree()
    {
        var root = new ListBubblingNode<object>("Root");
        var users = new ListBubblingNode<User>("Users");
        var settings = new DictionaryBubblingNode<string, object>("Settings");

        root.AttachChild(users);
        root.AttachChild(settings);

        users.Add(new User { Name = "Alice", Email = "alice@example.com" });
        users.Add(new User { Name = "Bob", Email = "bob@example.com" });
        settings.Put("Theme", "Dark");
        settings.Put("Language", "zh-CN");

        return root;
    }

    static void PrintTree(IChangeNode node, int indent = 0)
    {
        var prefix = new string(' ', indent * 2);
        Console.WriteLine($"{prefix}- {node.NodeName}");

        foreach (var child in node.Children)
        {
            PrintTree(child, indent + 1);
        }
    }
}
```

输出：
```
Snapshot exported
Saved to snapshot.json (1234 bytes)

--- Simulating restart ---

Tree restored from snapshot
- Root
  - Users
  - Settings
```
