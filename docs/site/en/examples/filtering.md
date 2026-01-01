# Filtering Examples

This section shows how to use event filters.

## Property Name Filter

```csharp
using Apq.ChangeBubbling.Infrastructure.EventFiltering;
using Apq.ChangeBubbling.Messaging;

var filter = new PropertyNameFilter("Name", "Email");
ChangeMessenger.RegisterFilter("propertyFilter", filter);
```

## Node Name Filter

```csharp
var filter = new NodeNameFilter("Users", "Settings");
ChangeMessenger.RegisterFilter("nodeFilter", filter);
```

## Change Kind Filter

```csharp
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
ChangeMessenger.RegisterFilter("kindFilter", filter);
```

## Custom Filter

```csharp
public class BusinessHoursFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        var hour = DateTime.Now.Hour;
        return hour >= 9 && hour < 18;
    }
}

ChangeMessenger.RegisterFilter("businessHours", new BusinessHoursFilter());
```

## Complete Example

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

        // Only process add events for Users node
        ChangeMessenger.RegisterFilter("userAddFilter",
            new CompositeFilter(
                new NodeNameFilter("Users"),
                new ChangeKindFilter(NodeChangeKind.CollectionAdd)
            ));

        ChangeMessenger.AsObservable()
            .Subscribe(c => Console.WriteLine($"[Filtered] {c.Kind} - {c.NodeName}"));

        var users = new ListBubblingNode<User>("Users");
        var logs = new ListBubblingNode<string>("Logs");

        users.Add(new User { Name = "Alice" });  // Processed
        users.RemoveAt(0);                        // Not processed
        logs.Add("Log entry");                    // Not processed

        ChangeMessenger.RemoveFilter("userAddFilter");
    }
}
```
