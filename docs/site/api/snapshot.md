# 快照服务 API

## TreeSnapshotService

树快照服务，用于导出和导入节点树状态。

### 静态方法

```csharp
// 导出节点树快照
static NodeSnapshot Export(IChangeNode root);

// 从快照创建新节点树
static IChangeNode Import(NodeSnapshot snapshot);

// 将快照导入到现有节点
static void ImportInto(IChangeNode target, NodeSnapshot snapshot);
```

**示例：**
```csharp
// 导出
var snapshot = TreeSnapshotService.Export(root);

// 导入
var newRoot = TreeSnapshotService.Import(snapshot);

// 导入到现有节点
TreeSnapshotService.ImportInto(existingRoot, snapshot);
```

## NodeSnapshot

节点快照类。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `NodeName` | `string` | 节点名称 |
| `NodeType` | `string` | 节点类型 |
| `Properties` | `Dictionary<string, object>` | 节点属性 |
| `Children` | `List<NodeSnapshot>` | 子节点快照 |

## SnapshotSerializer

快照序列化器。

### 静态方法

```csharp
// 序列化为 JSON
static string ToJson(NodeSnapshot snapshot);

// 从 JSON 反序列化
static NodeSnapshot FromJson(string json);

// 序列化为二进制
static byte[] ToBinary(NodeSnapshot snapshot);

// 从二进制反序列化
static NodeSnapshot FromBinary(byte[] data);
```

**示例：**
```csharp
// JSON 序列化
var json = SnapshotSerializer.ToJson(snapshot);
var loaded = SnapshotSerializer.FromJson(json);

// 二进制序列化
var bytes = SnapshotSerializer.ToBinary(snapshot);
var loaded = SnapshotSerializer.FromBinary(bytes);
```

## ISnapshotSerializable

可序列化接口，用于自定义节点的序列化行为。

```csharp
public interface ISnapshotSerializable
{
    Dictionary<string, object> GetSnapshotProperties();
    void ApplySnapshotProperties(Dictionary<string, object> properties);
}
```

**示例：**
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

## MultiValueSnapshotService

多值快照服务。

### 方法

```csharp
// 添加快照
void Add(string key, NodeSnapshot snapshot);

// 获取快照
NodeSnapshot Get(string key);

// 比较快照
SnapshotDiff Compare(string key1, string key2);
```
