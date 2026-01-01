# Basic Examples

This section shows basic usage of Apq.ChangeBubbling.

## Create Node Tree

```csharp
using Apq.ChangeBubbling.Nodes;

var root = new ListBubblingNode<object>("Root");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);
```

## Subscribe to Changes

```csharp
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
};
```

## Operate List Node

```csharp
users.Add(new User { Name = "Alice" });
users.Insert(0, new User { Name = "Bob" });
users.RemoveAt(0);
```

## Operate Dictionary Node

```csharp
settings.Put("Theme", "Dark");
settings.Put("FontSize", 14);

if (settings.TryGet("Theme", out var theme))
{
    Console.WriteLine($"Theme: {theme}");
}

settings.Remove("FontSize");
```

## Complete Example

```csharp
using Apq.ChangeBubbling.Nodes;

public class Program
{
    public static void Main()
    {
        var root = new ListBubblingNode<object>("Root");
        var users = new ListBubblingNode<User>("Users");
        var settings = new DictionaryBubblingNode<string, object>("Settings");

        root.AttachChild(users);
        root.AttachChild(settings);

        root.NodeChanged += (sender, change) =>
        {
            Console.WriteLine($"[{change.Kind}] {change.Path}");
        };

        users.Add(new User { Name = "Alice" });
        settings.Put("Theme", "Dark");

        Console.WriteLine($"Users: {users.Count}");
        Console.WriteLine($"Settings: {settings.Count}");
    }
}
```
