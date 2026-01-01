# 节点类型

Apq.ChangeBubbling 提供多种节点类型，用于构建树形数据结构。

## 基础节点

### BubblingNodeBase

所有节点的抽象基类，提供：
- 节点名称 (`NodeName`)
- 父子关系管理
- 事件冒泡机制

```csharp
public abstract class BubblingNodeBase : IChangeNode
{
    public string NodeName { get; }
    public IChangeNode? Parent { get; }
    public IReadOnlyList<IChangeNode> Children { get; }
}
```

## 集合节点

### ListBubblingNode&lt;T&gt;

列表节点，用于管理有序集合：

```csharp
var users = new ListBubblingNode<User>("Users");

// 添加元素
users.Add(new User { Name = "Alice" });
users.Insert(0, new User { Name = "Bob" });

// 移除元素
users.RemoveAt(0);
users.Remove(user);

// 访问元素
var count = users.Count;
var items = users.Items;
```

### DictionaryBubblingNode&lt;TKey, TValue&gt;

字典节点，用于管理键值对集合：

```csharp
var settings = new DictionaryBubblingNode<string, object>("Settings");

// 添加/更新
settings.Put("Theme", "Dark");
settings.Put("FontSize", 14);

// 获取
if (settings.TryGet("Theme", out var theme))
{
    Console.WriteLine($"Theme: {theme}");
}

// 移除
settings.Remove("Theme");
```

## 并发节点

### ConcurrentBagBubblingNode&lt;T&gt;

线程安全的列表节点：

```csharp
var logs = new ConcurrentBagBubblingNode<LogEntry>("Logs");

// 多线程安全操作
Parallel.For(0, 100, i =>
{
    logs.Add(new LogEntry { Message = $"Log {i}" });
});
```

### ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;

线程安全的字典节点：

```csharp
var cache = new ConcurrentDictionaryBubblingNode<string, object>("Cache");

// 多线程安全操作
Parallel.For(0, 100, i =>
{
    cache.Put($"Key{i}", $"Value{i}");
});
```

## 节点树构建

```csharp
// 创建根节点
var root = new ListBubblingNode<object>("Root");

// 创建子节点
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

// 附加子节点
root.AttachChild(users);
root.AttachChild(settings);

// 订阅根节点，接收所有子节点的变更
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}.{change.PropertyName}");
};
```

## 静默操作

使用 `PopulateSilently` 方法批量填充数据而不触发事件：

```csharp
var users = new ListBubblingNode<User>("Users");

// 静默填充，不触发事件
users.PopulateSilently(new[]
{
    new User { Name = "Alice" },
    new User { Name = "Bob" },
    new User { Name = "Charlie" }
});
```
