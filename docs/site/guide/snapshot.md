# 快照服务

Apq.ChangeBubbling 提供快照服务，用于导出和导入节点树的状态。

## 概述

快照服务可以：
- 导出节点树的当前状态
- 从快照恢复节点树
- 序列化/反序列化快照

## TreeSnapshotService

### 导出快照

```csharp
using Apq.ChangeBubbling.Snapshot;

var root = new ListBubblingNode<object>("Root");
// ... 构建节点树 ...

// 导出快照
var snapshot = TreeSnapshotService.Export(root);
```

### 导入快照

```csharp
// 从快照创建新节点树
var newRoot = TreeSnapshotService.Import(snapshot);

// 或导入到现有节点
TreeSnapshotService.ImportInto(existingNode, snapshot);
```

## NodeSnapshot 结构

```csharp
public class NodeSnapshot
{
    // 节点名称
    public string NodeName { get; set; }

    // 节点类型
    public string NodeType { get; set; }

    // 节点属性
    public Dictionary<string, object> Properties { get; set; }

    // 子节点快照
    public List<NodeSnapshot> Children { get; set; }
}
```

## 序列化

### JSON 序列化

```csharp
using Apq.ChangeBubbling.Snapshot;

// 导出快照
var snapshot = TreeSnapshotService.Export(root);

// 序列化为 JSON
var json = SnapshotSerializer.ToJson(snapshot);

// 保存到文件
File.WriteAllText("snapshot.json", json);

// 从 JSON 反序列化
var loadedSnapshot = SnapshotSerializer.FromJson(json);

// 恢复节点树
var restoredRoot = TreeSnapshotService.Import(loadedSnapshot);
```

### 二进制序列化

```csharp
// 序列化为二进制
var bytes = SnapshotSerializer.ToBinary(snapshot);

// 保存到文件
File.WriteAllBytes("snapshot.bin", bytes);

// 从二进制反序列化
var loadedSnapshot = SnapshotSerializer.FromBinary(bytes);
```

## 自定义序列化

实现 `ISnapshotSerializable` 接口来自定义节点的序列化行为：

```csharp
public class CustomNode : BubblingNodeBase, ISnapshotSerializable
{
    public string CustomProperty { get; set; }

    public Dictionary<string, object> GetSnapshotProperties()
    {
        return new Dictionary<string, object>
        {
            ["CustomProperty"] = CustomProperty,
            ["Timestamp"] = DateTime.Now
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

## MultiValueSnapshotService

用于处理多值快照：

```csharp
using Apq.ChangeBubbling.Snapshot;

// 创建多值快照服务
var service = new MultiValueSnapshotService();

// 添加多个快照
service.Add("v1", TreeSnapshotService.Export(root));
service.Add("v2", TreeSnapshotService.Export(root));

// 获取快照
var snapshot = service.Get("v1");

// 比较快照
var diff = service.Compare("v1", "v2");
```

## 增量快照

```csharp
// 保存初始快照
var initialSnapshot = TreeSnapshotService.Export(root);

// ... 进行一些修改 ...

// 保存增量快照
var incrementalSnapshot = TreeSnapshotService.ExportIncremental(root, initialSnapshot);

// 应用增量快照
TreeSnapshotService.ApplyIncremental(root, incrementalSnapshot);
```

## 最佳实践

1. **定期保存快照**：用于数据恢复
2. **使用增量快照**：减少存储空间
3. **压缩大型快照**：使用 GZip 压缩
4. **验证快照完整性**：使用校验和

```csharp
// 压缩快照
var json = SnapshotSerializer.ToJson(snapshot);
var compressed = Compress(json);
File.WriteAllBytes("snapshot.json.gz", compressed);

// 解压并恢复
var decompressed = Decompress(File.ReadAllBytes("snapshot.json.gz"));
var snapshot = SnapshotSerializer.FromJson(decompressed);
```
