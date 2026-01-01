# Event Filtering

Apq.ChangeBubbling provides flexible event filtering mechanisms.

## Filter Interface

```csharp
public interface IChangeEventFilter
{
    bool ShouldProcess(BubblingChange change);
}
```

## Built-in Filters

### PropertyNameFilter

```csharp
var filter = new PropertyNameFilter("Name", "Email");
ChangeMessenger.RegisterFilter("propertyFilter", filter);
```

### NodeNameFilter

```csharp
var filter = new NodeNameFilter("Users", "Settings");
ChangeMessenger.RegisterFilter("nodeFilter", filter);
```

### ChangeKindFilter

```csharp
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
ChangeMessenger.RegisterFilter("kindFilter", filter);
```

### ThrottleFilter

```csharp
var filter = new ThrottleFilter(TimeSpan.FromMilliseconds(100));
ChangeMessenger.RegisterFilter("throttleFilter", filter);
```

## Custom Filters

```csharp
public class CustomFilter : IChangeEventFilter
{
    public bool ShouldProcess(BubblingChange change)
    {
        return change.NewValue != null;
    }
}

ChangeMessenger.RegisterFilter("customFilter", new CustomFilter());
```

## Managing Filters

```csharp
// Register
ChangeMessenger.RegisterFilter("myFilter", filter);

// Remove
ChangeMessenger.RemoveFilter("myFilter");
```
