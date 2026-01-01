# 事件过滤

Apq.ChangeBubbling 提供灵活的事件过滤机制，可以在消息发布前筛选事件。

## 过滤器接口

```csharp
public interface IChangeEventFilter
{
    bool ShouldProcess(BubblingChange change);
}
```

## 内置过滤器

### 属性名称过滤器

只处理指定属性的变更：

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;

// 只处理 Name 和 Email 属性的变更
var filter = new PropertyNameFilter("Name", "Email");
ChangeMessenger.RegisterFilter("propertyFilter", filter);
```

### 节点名称过滤器

只处理指定节点的变更：

```csharp
var filter = new NodeNameFilter("Users", "Settings");
ChangeMessenger.RegisterFilter("nodeFilter", filter);
```

### 变更类型过滤器

只处理指定类型的变更：

```csharp
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
ChangeMessenger.RegisterFilter("kindFilter", filter);
```

### 路径过滤器

基于路径模式过滤：

```csharp
// 只处理 Users 下的变更
var filter = new PathFilter("Root/Users/*");
ChangeMessenger.RegisterFilter("pathFilter", filter);
```

### 频率过滤器

限制事件频率：

```csharp
// 每 100ms 最多处理一个事件
var filter = new ThrottleFilter(TimeSpan.FromMilliseconds(100));
ChangeMessenger.RegisterFilter("throttleFilter", filter);
```

## 自定义过滤器

```csharp
public class CustomFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        // 只处理新值不为 null 的变更
        if (change.NewValue == null)
            return false;

        // 只处理工作时间内的变更
        var hour = DateTime.Now.Hour;
        if (hour < 9 || hour > 18)
            return false;

        return true;
    }
}

// 注册自定义过滤器
ChangeMessenger.RegisterFilter("customFilter", new CustomFilter());
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
        // 所有过滤器都通过才处理
        return _filters.All(f => f.ShouldProcess(change));
    }
}

// 组合多个过滤器
var compositeFilter = new CompositeFilter(
    new PropertyNameFilter("Name"),
    new ChangeKindFilter(NodeChangeKind.PropertyChanged),
    new ThrottleFilter(TimeSpan.FromMilliseconds(50))
);

ChangeMessenger.RegisterFilter("composite", compositeFilter);
```

## 管理过滤器

```csharp
// 注册过滤器
ChangeMessenger.RegisterFilter("myFilter", filter);

// 移除过滤器
ChangeMessenger.RemoveFilter("myFilter");
```

## 使用 Rx 进行过滤

除了使用过滤器，也可以使用 Rx 操作符进行过滤：

```csharp
ChangeMessenger.AsObservable()
    // 按属性名称过滤
    .Where(c => c.PropertyName == "Name")
    // 按变更类型过滤
    .Where(c => c.Kind == NodeChangeKind.PropertyChanged)
    // 节流
    .Throttle(TimeSpan.FromMilliseconds(100))
    // 去重
    .DistinctUntilChanged(c => c.NewValue)
    .Subscribe(c => HandleChange(c));
```

## 过滤器 vs Rx 操作符

| 特性 | 过滤器 | Rx 操作符 |
|------|--------|-----------|
| 执行时机 | 发布前 | 订阅时 |
| 影响范围 | 全局 | 单个订阅 |
| 性能 | 更高（减少发布） | 较低（每个订阅都处理） |
| 灵活性 | 较低 | 更高 |
| 适用场景 | 全局过滤 | 特定订阅过滤 |
