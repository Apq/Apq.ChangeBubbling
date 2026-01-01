# 最佳实践

本文总结 Apq.ChangeBubbling 的最佳实践。

## 节点设计

### 合理组织节点树

```csharp
// 推荐：按业务领域组织
var root = new ListBubblingNode<object>("App");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");
var logs = new ConcurrentBagBubblingNode<LogEntry>("Logs");

root.AttachChild(users);
root.AttachChild(settings);
root.AttachChild(logs);
```

### 选择合适的节点类型

| 场景 | 推荐节点类型 |
|------|-------------|
| 有序列表 | `ListBubblingNode<T>` |
| 键值对 | `DictionaryBubblingNode<TKey, TValue>` |
| 多线程列表 | `ConcurrentBagBubblingNode<T>` |
| 多线程字典 | `ConcurrentDictionaryBubblingNode<TKey, TValue>` |

## 事件处理

### 使用 Rx 流处理复杂逻辑

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users")
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => HandleUserChange(c));
```

### 避免在事件处理中执行耗时操作

```csharp
// 不推荐
root.NodeChanged += (s, c) =>
{
    SaveToDatabase(c); // 耗时操作
};

// 推荐：使用异步处理
ChangeMessenger.AsObservable()
    .SelectMany(c => Observable.FromAsync(() => SaveToDatabaseAsync(c)))
    .Subscribe();
```

### 及时取消订阅

```csharp
public class MyViewModel : IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    public MyViewModel()
    {
        _disposables.Add(
            ChangeMessenger.AsObservable()
                .Subscribe(c => HandleChange(c))
        );
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
```

## 性能优化

### 初始化数据使用静默填充

```csharp
// 加载数据时不触发事件
users.PopulateSilently(await LoadUsersAsync());
```

### 批量更新使用批量操作

```csharp
using (users.BeginBatchScope())
{
    foreach (var user in updatedUsers)
    {
        users.Add(user);
    }
}
```

### 使用事件过滤

```csharp
// 只处理需要的事件
ChangeMessenger.RegisterFilter("filter",
    new PropertyNameFilter("Name", "Email"));
```

## 错误处理

### 使用 Rx 错误处理

```csharp
ChangeMessenger.AsObservable()
    .Do(c => Process(c))
    .Catch<BubblingChange, Exception>(ex =>
    {
        Logger.Error(ex);
        return Observable.Empty<BubblingChange>();
    })
    .Retry(3)
    .Subscribe();
```

### 使用 try-finally 确保批量操作完成

```csharp
users.BeginBatch();
try
{
    // 可能抛出异常的操作
    ProcessUsers(users);
}
finally
{
    users.EndBatch(); // 确保调用
}
```

## 测试

### 使用同步发布进行测试

```csharp
[SetUp]
public void Setup()
{
    ChangeMessenger.UseSynchronousPublish = true;
}

[Test]
public void TestUserAdd()
{
    var changes = new List<BubblingChange>();
    ChangeMessenger.AsObservable()
        .Subscribe(c => changes.Add(c));

    users.Add(new User { Name = "Test" });

    Assert.That(changes, Has.Count.EqualTo(1));
    Assert.That(changes[0].Kind, Is.EqualTo(NodeChangeKind.CollectionAdd));
}
```

## 调试

### 启用性能指标

```csharp
ChangeMessenger.EnableMetrics = true;

// 定期输出性能指标
Observable.Interval(TimeSpan.FromSeconds(10))
    .Subscribe(_ =>
    {
        var metrics = ChangeMessenger.GetPerformanceMetrics();
        Console.WriteLine($"Events: {metrics.TotalEvents}, Rate: {metrics.EventsPerSecond}/s");
    });
```

### 记录事件日志

```csharp
ChangeMessenger.AsObservable()
    .Do(c => Logger.Debug($"[{c.Kind}] {c.Path}"))
    .Subscribe();
```
