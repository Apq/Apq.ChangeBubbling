# Examples Overview

This section provides usage examples for Apq.ChangeBubbling.

## Example List

| Example | Description |
|---------|-------------|
| [Basic Examples](/en/examples/basic) | Node creation, event subscription, basic operations |
| [Reactive](/en/examples/reactive) | Reactive streams, throttling, buffering |
| [Filtering](/en/examples/filtering) | Property, path, and frequency filtering |
| [Snapshot](/en/examples/snapshot) | Snapshot export and import |

## Quick Example

### Create Node Tree

```csharp
using Apq.ChangeBubbling.Nodes;

var root = new ListBubblingNode<string>("Root");
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

root.AttachChild(users);
root.AttachChild(settings);
```

### Subscribe to Changes

```csharp
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}.{change.PropertyName}");
};
```

### Use Rx Streams

```csharp
using Apq.ChangeBubbling.Messaging;
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => Console.WriteLine($"Added: {c.PropertyName}"));
```

## Run Examples

The sample project is located in `Samples/Apq.ChangeBubbling.Samples`:

```bash
cd Samples/Apq.ChangeBubbling.Samples
dotnet run
```
