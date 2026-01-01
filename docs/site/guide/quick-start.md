# 快速开始

本指南将帮助你在 5 分钟内上手 Apq.ChangeBubbling。

## 安装

使用 .NET CLI 安装：

```bash
dotnet add package Apq.ChangeBubbling
```

或在项目文件中添加：

```xml
<PackageReference Include="Apq.ChangeBubbling" Version="1.0.*" />
```

## 创建第一个节点树

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建根节点
var root = new ListBubblingNode<string>("Root");

// 创建子节点
var users = new ListBubblingNode<string>("Users");
var settings = new DictionaryBubblingNode<string, int>("Settings");

// 建立父子关系
root.AttachChild(users);
root.AttachChild(settings);
```

## 订阅变更事件

```csharp
// 在根节点订阅，可以接收所有子节点的变更
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"节点: {change.NodeName}");
    Console.WriteLine($"属性: {change.PropertyName}");
    Console.WriteLine($"类型: {change.Kind}");
    Console.WriteLine($"路径: {string.Join(".", change.PathSegments)}");
    Console.WriteLine("---");
};
```

## 触发变更

```csharp
// 添加用户
users.Add("Alice");
users.Add("Bob");

// 设置配置
settings["MaxRetries"] = 3;
settings["Timeout"] = 5000;
```

输出：

```
节点: Users
属性: 0
类型: CollectionAdd
路径: Users.0
---
节点: Users
属性: 1
类型: CollectionAdd
路径: Users.1
---
节点: Settings
属性: MaxRetries
类型: CollectionAdd
路径: Settings.MaxRetries
---
节点: Settings
属性: Timeout
类型: CollectionAdd
路径: Settings.Timeout
---
```

## 使用 Rx 响应式流

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

// 订阅所有变更
ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(change =>
    {
        Console.WriteLine($"新增: {change.PropertyName}");
    });

// 节流订阅（100ms 内只处理一次）
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(change =>
    {
        Console.WriteLine($"节流后: {change.PropertyName}");
    });
```

## 批量操作

```csharp
var node = new ListBubblingNode<int>("Numbers");

// 开始批量操作
node.BeginBatch();
try
{
    for (int i = 0; i < 1000; i++)
    {
        node.Add(i);
    }
}
finally
{
    // 结束批量操作，一次性触发所有事件
    node.EndBatch();
}
```

## 下一步

- [节点类型](/guide/node-types) - 了解所有可用的节点类型
- [事件冒泡](/guide/event-bubbling) - 深入理解冒泡机制
- [Rx 响应式流](/guide/reactive-streams) - 高级响应式编程
