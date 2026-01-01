# Filtering API

## IChangeEventFilter

Filter interface.

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
```

### NodeNameFilter

```csharp
var filter = new NodeNameFilter("Users", "Settings");
```

### ChangeKindFilter

```csharp
var filter = new ChangeKindFilter(
    NodeChangeKind.CollectionAdd,
    NodeChangeKind.CollectionRemove
);
```

### PathFilter

```csharp
var filter = new PathFilter("Root/Users/*");
```

### ThrottleFilter

```csharp
var filter = new ThrottleFilter(TimeSpan.FromMilliseconds(100));
```

## Using Filters

```csharp
// Register
ChangeMessenger.RegisterFilter("myFilter", filter);

// Remove
ChangeMessenger.RemoveFilter("myFilter");
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
```
