# 事件过滤示例

本节展示如何使用事件过滤器筛选变更事件。

## 属性名称过滤

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;
using Apq.ChangeBubbling.Messaging;

// 只处理 Name 和 Email 属性的变更
var filter = new PropertyNameFilter("Name", "Email");
ChangeMessenger.RegisterFilter("propertyFilter", filter);

// 现在只有 Name 和 Email 的变更会被发布
```

## 节点名称过滤

```csharp
// 只处理 Users 和 Settings 节点的变更
var filter = new NodeNameFilter("Users", "Settings");
ChangeMessenger.RegisterFilter("nodeFilter", filter);
```

## 变更类型过滤

```csharp
// 只处理添加和移除事件
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
ChangeMessenger.RegisterFilter("kindFilter", filter);
```

## 自定义过滤器

```csharp
public class BusinessHoursFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        var hour = DateTime.Now.Hour;
        // 只在工作时间（9:00-18:00）处理事件
        return hour >= 9 && hour < 18;
    }
}

ChangeMessenger.RegisterFilter("businessHours", new BusinessHoursFilter());
```

## 组合过滤器

```csharp
public class CompositeFilter : IChangeEventFilter
{
    private readonly IChangeEventFilter[] _filters;

    public CompositeFilter(params IChangeEventFilter[] filters)
    {
        _filters = filters;
    }

    public bool ShouldProcess(BubblingChange change)
    {
        return _filters.All(f => f.ShouldProcess(change));
    }
}

// 组合多个条件
var filter = new CompositeFilter(
    new NodeNameFilter("Users"),
    new ChangeKindFilter(NodeChangeKind.CollectionAdd),
    new BusinessHoursFilter()
);

ChangeMessenger.RegisterFilter("composite", filter);
```

## 完整示例

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Messaging;
using Apq.ChangeBubbling.Infrastructure.EventFiltering;
using System.Reactive.Linq;

public class Program
{
    public static void Main()
    {
        ChangeMessenger.EnableRxStream = true;

        // 注册过滤器：只处理 Users 节点的添加事件
        ChangeMessenger.RegisterFilter("userAddFilter",
            new CompositeFilter(
                new NodeNameFilter("Users"),
                new ChangeKindFilter(NodeChangeKind.CollectionAdd)
            ));

        // 订阅（只会收到过滤后的事件）
        ChangeMessenger.AsObservable()
            .Subscribe(c =>
            {
                Console.WriteLine($"[Filtered] {c.Kind} - {c.NodeName}");
            });

        // 创建节点
        var users = new ListBubblingNode<User>("Users");
        var logs = new ListBubblingNode<string>("Logs");

        // 操作数据
        users.Add(new User { Name = "Alice" });  // 会被处理
        users.Add(new User { Name = "Bob" });    // 会被处理
        users.RemoveAt(0);                        // 不会被处理（不是添加）
        logs.Add("Log entry");                    // 不会被处理（不是 Users）

        // 移除过滤器
        ChangeMessenger.RemoveFilter("userAddFilter");

        // 现在所有事件都会被处理
        users.Add(new User { Name = "Charlie" });
        logs.Add("Another log");
    }
}
```

输出：
```
[Filtered] CollectionAdd - Users
[Filtered] CollectionAdd - Users
```
