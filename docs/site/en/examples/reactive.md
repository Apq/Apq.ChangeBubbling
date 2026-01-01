# Reactive Examples

This section shows how to use System.Reactive with Apq.ChangeBubbling.

## Basic Subscription

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.EnableRxStream = true;

ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine($"[{change.Kind}] {change.NodeName}"));
```

## Filtering

```csharp
// By change type
ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));

// By node name
ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users")
    .Subscribe(c => Console.WriteLine($"User change: {c.Kind}"));
```

## Throttling

```csharp
ChangeMessenger.AsObservable()
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => SaveChanges(c));
```

## Buffering

```csharp
ChangeMessenger.AsObservable()
    .Buffer(TimeSpan.FromSeconds(1))
    .Where(batch => batch.Count > 0)
    .Subscribe(batch =>
    {
        Console.WriteLine($"Processing {batch.Count} changes");
    });
```

## Complete Example

```csharp
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

public class Program
{
    public static void Main()
    {
        ChangeMessenger.EnableRxStream = true;

        var users = new ListBubblingNode<User>("Users");

        ChangeMessenger.AsObservable()
            .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
            .Subscribe(c => Console.WriteLine($"User added: {c.NewValue}"));

        ChangeMessenger.AsObservable()
            .Buffer(TimeSpan.FromSeconds(1))
            .Where(batch => batch.Count > 0)
            .Subscribe(batch =>
            {
                Console.WriteLine($"[Stats] {batch.Count} changes");
            });

        users.Add(new User { Name = "Alice" });
        users.Add(new User { Name = "Bob" });

        Console.ReadLine();
    }
}
```
