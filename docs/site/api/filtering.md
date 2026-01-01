# 事件过滤 API

## IChangeEventFilter

过滤器接口。

```csharp
public interface IChangeEventFilter
{
    bool ShouldProcess(BubblingChange change);
}
```

## 内置过滤器

### PropertyNameFilter

按属性名称过滤。

```csharp
public class PropertyNameFilter : IChangeEventFilter
{
    public PropertyNameFilter(params string[] propertyNames);
    public bool ShouldProcess(BubblingChange change);
}
```

**示例：**
```csharp
var filter = new PropertyNameFilter("Name", "Email");
```

### NodeNameFilter

按节点名称过滤。

```csharp
public class NodeNameFilter : IChangeEventFilter
{
    public NodeNameFilter(params string[] nodeNames);
    public bool ShouldProcess(BubblingChange change);
}
```

**示例：**
```csharp
var filter = new NodeNameFilter("Users", "Settings");
```

### ChangeKindFilter

按变更类型过滤。

```csharp
public class ChangeKindFilter : IChangeEventFilter
{
    public ChangeKindFilter(params NodeChangeKind[] kinds);
    public bool ShouldProcess(BubblingChange change);
}
```

**示例：**
```csharp
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
```

### PathFilter

按路径模式过滤。

```csharp
public class PathFilter : IChangeEventFilter
{
    public PathFilter(string pattern);
    public bool ShouldProcess(BubblingChange change);
}
```

**示例：**
```csharp
var filter = new PathFilter("Root/Users/*");
```

### ThrottleFilter

频率限制过滤器。

```csharp
public class ThrottleFilter : IChangeEventFilter
{
    public ThrottleFilter(TimeSpan interval);
    public bool ShouldProcess(BubblingChange change);
}
```

**示例：**
```csharp
var filter = new ThrottleFilter(TimeSpan.FromMilliseconds(100));
```

## 使用过滤器

```csharp
// 注册过滤器
ChangeMessenger.RegisterFilter("myFilter", filter);

// 移除过滤器
ChangeMessenger.RemoveFilter("myFilter");
```

## 自定义过滤器

```csharp
public class CustomFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        // 自定义过滤逻辑
        return change.NewValue != null;
    }
}
```
