# Rx 响应式示例

本节展示如何使用 System.Reactive 处理变更事件。

## 基本订阅

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

// 启用 Rx 流
ChangeMessenger.EnableRxStream = true;

// 订阅所有变更
ChangeMessenger.AsObservable()
    .Subscribe(change =>
    {
        Console.WriteLine($"[{change.Kind}] {change.NodeName}");
    });
```

## 过滤事件

```csharp
// 只处理添加事件
ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));

// 只处理特定节点
ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users")
    .Subscribe(c => Console.WriteLine($"User change: {c.Kind}"));

// 只处理特定属性
ChangeMessenger.AsObservable()
    .Where(c => c.PropertyName == "Name")
    .Subscribe(c => Console.WriteLine($"Name changed: {c.OldValue} -> {c.NewValue}"));
```

## 节流与防抖

```csharp
// 节流：每 100ms 最多处理一个事件
ChangeMessenger.AsObservable()
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => SaveChanges(c));

// 防抖：等待 300ms 无新事件后处理
ChangeMessenger.AsObservable()
    .Debounce(TimeSpan.FromMilliseconds(300))
    .Subscribe(c => Search(c.NewValue?.ToString()));
```

## 缓冲批处理

```csharp
// 每秒批量处理
ChangeMessenger.AsObservable()
    .Buffer(TimeSpan.FromSeconds(1))
    .Where(batch => batch.Count > 0)
    .Subscribe(batch =>
    {
        Console.WriteLine($"Processing {batch.Count} changes");
        foreach (var change in batch)
        {
            Process(change);
        }
    });

// 每 10 个事件批量处理
ChangeMessenger.AsObservable()
    .Buffer(10)
    .Subscribe(batch => ProcessBatch(batch));
```

## 转换数据

```csharp
// 转换为 DTO
ChangeMessenger.AsObservable()
    .Select(c => new ChangeDto
    {
        Type = c.Kind.ToString(),
        Node = c.NodeName,
        Path = c.Path,
        Value = c.NewValue
    })
    .Subscribe(dto => SendToServer(dto));
```

## 组合流

```csharp
// 合并多个流
var userChanges = ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users");

var settingChanges = ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Settings");

userChanges.Merge(settingChanges)
    .Subscribe(c => LogChange(c));
```

## 完整示例

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

public class Program
{
    public static void Main()
    {
        // 启用 Rx 流
        ChangeMessenger.EnableRxStream = true;

        // 创建节点
        var users = new ListBubblingNode<User>("Users");

        // 订阅添加事件
        ChangeMessenger.AsObservable()
            .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
            .Subscribe(c => Console.WriteLine($"User added: {c.NewValue}"));

        // 订阅移除事件
        ChangeMessenger.AsObservable()
            .Where(c => c.Kind == NodeChangeKind.CollectionRemove)
            .Subscribe(c => Console.WriteLine($"User removed: {c.OldValue}"));

        // 批量统计（每秒）
        ChangeMessenger.AsObservable()
            .Buffer(TimeSpan.FromSeconds(1))
            .Where(batch => batch.Count > 0)
            .Subscribe(batch =>
            {
                Console.WriteLine($"[Stats] {batch.Count} changes in last second");
            });

        // 操作数据
        users.Add(new User { Name = "Alice" });
        users.Add(new User { Name = "Bob" });
        users.Add(new User { Name = "Charlie" });
        users.RemoveAt(0);

        Console.ReadLine();
    }
}
```
