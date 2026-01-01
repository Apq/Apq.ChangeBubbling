# Best Practices

This document summarizes best practices for Apq.ChangeBubbling.

## Node Design

### Organize Node Tree by Domain

```csharp
var root = new ListBubblingNode<object>("App");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);
```

### Choose Appropriate Node Types

| Scenario | Recommended Type |
|----------|-----------------|
| Ordered list | `ListBubblingNode<T>` |
| Key-value pairs | `DictionaryBubblingNode<TKey, TValue>` |
| Multi-threaded list | `ConcurrentBagBubblingNode<T>` |
| Multi-threaded dictionary | `ConcurrentDictionaryBubblingNode<TKey, TValue>` |

## Event Handling

### Use Rx for Complex Logic

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users")
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => HandleUserChange(c));
```

### Avoid Long Operations in Handlers

```csharp
// Recommended: async processing
ChangeMessenger.AsObservable()
    .SelectMany(c => Observable.FromAsync(() => SaveToDatabaseAsync(c)))
    .Subscribe();
```

### Dispose Subscriptions

```csharp
public class MyViewModel : IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
```

## Performance

1. Use `PopulateSilently` for initialization
2. Use batch operations for bulk updates
3. Use event filters to reduce unnecessary processing
4. Use concurrent nodes for multi-threading

## Error Handling

```csharp
ChangeMessenger.AsObservable()
    .Catch<BubblingChange, Exception>(ex =>
    {
        Logger.Error(ex);
        return Observable.Empty<BubblingChange>();
    })
    .Retry(3)
    .Subscribe();
```

## Testing

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
}
```
