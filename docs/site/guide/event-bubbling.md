# 事件冒泡

事件冒泡是 Apq.ChangeBubbling 的核心机制，允许子节点的变更事件向上传播到父节点。

## 冒泡机制

当子节点发生变更时，事件会自动向上冒泡到所有祖先节点：

```
Root (收到所有事件)
├── Users (收到 Users 及其子节点的事件)
│   ├── User1
│   └── User2
└── Settings (收到 Settings 及其子节点的事件)
    ├── Theme
    └── Language
```

## 变更类型

`NodeChangeKind` 枚举定义了所有变更类型：

| 类型 | 说明 |
|------|------|
| `PropertyChanged` | 属性值变更 |
| `CollectionAdd` | 集合添加元素 |
| `CollectionRemove` | 集合移除元素 |
| `CollectionReplace` | 集合替换元素 |
| `CollectionClear` | 集合清空 |
| `ChildAttached` | 子节点附加 |
| `ChildDetached` | 子节点分离 |

## 订阅事件

### 直接订阅

```csharp
var root = new ListBubblingNode<object>("Root");

root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
    Console.WriteLine($"  Property: {change.PropertyName}");
    Console.WriteLine($"  Path: {change.Path}");
    Console.WriteLine($"  OldValue: {change.OldValue}");
    Console.WriteLine($"  NewValue: {change.NewValue}");
};
```

### 使用 Rx 流

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));
```

## BubblingChange 结构

```csharp
public readonly struct BubblingChange
{
    // 变更类型
    public NodeChangeKind Kind { get; }

    // 发生变更的节点名称
    public string NodeName { get; }

    // 变更的属性名称
    public string? PropertyName { get; }

    // 从根节点到当前节点的路径
    public string Path { get; }

    // 旧值
    public object? OldValue { get; }

    // 新值
    public object? NewValue { get; }

    // 时间戳
    public DateTime Timestamp { get; }
}
```

## 路径追踪

每个变更事件都包含完整的路径信息：

```csharp
root.NodeChanged += (sender, change) =>
{
    // 路径格式: Root/Users/[0] 或 Root/Settings/Theme
    Console.WriteLine($"Path: {change.Path}");
};

// 列表节点使用索引路径
users.Add(new User()); // Path: Root/Users/[0]

// 字典节点使用键路径
settings.Put("Theme", "Dark"); // Path: Root/Settings/Theme
```

## 阻止冒泡

在某些场景下，可能需要阻止事件继续冒泡：

```csharp
// 使用批量操作，只在结束时触发一次事件
node.BeginBatch();
try
{
    for (int i = 0; i < 100; i++)
    {
        node.Add(i);
    }
}
finally
{
    node.EndBatch(); // 触发一次批量变更事件
}
```
