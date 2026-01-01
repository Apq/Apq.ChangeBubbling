# 示例概述

本节提供 Apq.ChangeBubbling 的使用示例。

## 示例列表

| 示例 | 说明 |
|------|------|
| [基础示例](/examples/basic) | 节点创建、事件订阅、基本操作 |
| [Rx 响应式](/examples/reactive) | 响应式流、节流、缓冲 |
| [事件过滤](/examples/filtering) | 属性过滤、路径过滤、频率过滤 |
| [快照导出](/examples/snapshot) | 快照导出与导入 |

## 快速示例

### 创建节点树

```csharp
using Apq.ChangeBubbling.Nodes;

var root = new ListBubblingNode<string>("Root");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);
```

### 订阅变更

```csharp
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}.{change.PropertyName}");
};
```

### 使用 Rx 流

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => Console.WriteLine($"新增: {c.PropertyName}"));
```

### 批量操作

```csharp
var node = new ListBubblingNode<int>("Numbers");

node.BeginBatch();
for (int i = 0; i < 100; i++)
{
    node.Add(i);
}
node.EndBatch();  // 一次性触发所有事件
```

## 运行示例

示例项目位于 `Samples/Apq.ChangeBubbling.Samples` 目录：

```bash
cd Samples/Apq.ChangeBubbling.Samples
dotnet run
```
