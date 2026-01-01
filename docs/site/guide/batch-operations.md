# 批量操作

Apq.ChangeBubbling 支持批量操作，可以将多个变更合并为一次事件，提高性能。

## 基本用法

```csharp
var users = new ListBubblingNode<User>("Users");

// 开始批量操作
users.BeginBatch();

try
{
    // 这些操作不会立即触发事件
    for (int i = 0; i < 100; i++)
    {
        users.Add(new User { Name = $"User{i}" });
    }
}
finally
{
    // 结束批量操作，触发一次批量变更事件
    users.EndBatch();
}
```

## 使用 using 语句

```csharp
using (users.BeginBatchScope())
{
    users.Add(new User { Name = "Alice" });
    users.Add(new User { Name = "Bob" });
    users.Add(new User { Name = "Charlie" });
} // 自动调用 EndBatch
```

## 嵌套批量操作

```csharp
users.BeginBatch();
try
{
    users.Add(user1);

    // 嵌套批量操作
    users.BeginBatch();
    try
    {
        users.Add(user2);
        users.Add(user3);
    }
    finally
    {
        users.EndBatch(); // 不触发事件（嵌套）
    }

    users.Add(user4);
}
finally
{
    users.EndBatch(); // 触发事件（最外层）
}
```

## 静默填充

使用 `PopulateSilently` 方法可以批量填充数据而完全不触发事件：

```csharp
var users = new ListBubblingNode<User>("Users");

// 静默填充，不触发任何事件
users.PopulateSilently(new[]
{
    new User { Name = "Alice" },
    new User { Name = "Bob" },
    new User { Name = "Charlie" }
});

// 字典节点也支持
var settings = new DictionaryBubblingNode<string, object>("Settings");
settings.PopulateSilently(new Dictionary<string, object>
{
    ["Theme"] = "Dark",
    ["FontSize"] = 14,
    ["Language"] = "zh-CN"
});
```

## 批量操作 vs 静默填充

| 特性 | 批量操作 | 静默填充 |
|------|----------|----------|
| 触发事件 | 结束时触发一次 | 不触发 |
| 适用场景 | 需要通知变更 | 初始化数据 |
| 性能 | 较高 | 最高 |

## 性能对比

```csharp
var users = new ListBubblingNode<User>("Users");
var data = Enumerable.Range(0, 10000)
    .Select(i => new User { Name = $"User{i}" })
    .ToList();

// 方式1：逐个添加（最慢，触发10000次事件）
foreach (var user in data)
{
    users.Add(user);
}

// 方式2：批量操作（较快，触发1次事件）
users.BeginBatch();
foreach (var user in data)
{
    users.Add(user);
}
users.EndBatch();

// 方式3：静默填充（最快，不触发事件）
users.PopulateSilently(data);
```

## 最佳实践

1. **初始化数据**：使用 `PopulateSilently`
2. **批量更新需要通知**：使用 `BeginBatch/EndBatch`
3. **单个操作**：直接调用方法
4. **避免在批量操作中抛出异常**：使用 try-finally 确保 EndBatch 被调用
