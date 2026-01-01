# Batch Operations

Apq.ChangeBubbling supports batch operations to combine multiple changes into a single event.

## Basic Usage

```csharp
var users = new ListBubblingNode<User>("Users");

users.BeginBatch();
try
{
    for (int i = 0; i < 100; i++)
    {
        users.Add(new User { Name = $"User{i}" });
    }
}
finally
{
    users.EndBatch(); // Triggers one batch change event
}
```

## Using Statement

```csharp
using (users.BeginBatchScope())
{
    users.Add(new User { Name = "Alice" });
    users.Add(new User { Name = "Bob" });
} // Automatically calls EndBatch
```

## Silent Population

Use `PopulateSilently` to populate data without triggering any events:

```csharp
users.PopulateSilently(new[]
{
    new User { Name = "Alice" },
    new User { Name = "Bob" }
});
```

## Batch vs Silent

| Feature | Batch | Silent |
|---------|-------|--------|
| Events | One at end | None |
| Use Case | Need notification | Initialization |
| Performance | High | Highest |

## Best Practices

1. **Initialization**: Use `PopulateSilently`
2. **Batch updates with notification**: Use `BeginBatch/EndBatch`
3. **Single operations**: Call methods directly
4. **Exception safety**: Use try-finally to ensure EndBatch is called
