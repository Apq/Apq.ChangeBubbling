# Reactive Streams

Apq.ChangeBubbling deeply integrates with System.Reactive for powerful reactive programming.

## Enable Rx Streams

```csharp
using Apq.ChangeBubbling.Messaging;

ChangeMessenger.EnableRxStream = true;
```

## Basic Subscription

```csharp
using System.Reactive.Linq;

ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine(change));
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
    .Subscribe(c => HandleUserChange(c));
```

## Throttling and Debouncing

```csharp
// Throttle
ChangeMessenger.AsObservable()
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => SaveChanges(c));

// Debounce
ChangeMessenger.AsObservable()
    .Debounce(TimeSpan.FromMilliseconds(300))
    .Subscribe(c => Search(c.NewValue?.ToString()));
```

## Buffering

```csharp
// Time buffer
ChangeMessenger.AsObservable()
    .Buffer(TimeSpan.FromSeconds(1))
    .Where(batch => batch.Count > 0)
    .Subscribe(batch => ProcessBatch(batch));

// Count buffer
ChangeMessenger.AsObservable()
    .Buffer(10)
    .Subscribe(batch => ProcessBatch(batch));
```

## Transformation

```csharp
ChangeMessenger.AsObservable()
    .Select(c => new ChangeDto
    {
        Type = c.Kind.ToString(),
        Node = c.NodeName,
        Value = c.NewValue
    })
    .Subscribe(dto => SendToServer(dto));
```

## Error Handling

```csharp
ChangeMessenger.AsObservable()
    .Do(c => Console.WriteLine($"Processing: {c}"))
    .Catch<BubblingChange, Exception>(ex =>
    {
        Console.WriteLine($"Error: {ex.Message}");
        return Observable.Empty<BubblingChange>();
    })
    .Retry(3)
    .Subscribe(c => ProcessChange(c));
```

## Resource Management

```csharp
var disposables = new CompositeDisposable();

disposables.Add(
    ChangeMessenger.AsObservable()
        .Where(c => c.NodeName == "Users")
        .Subscribe(c => HandleUserChange(c))
);

// Dispose all subscriptions
disposables.Dispose();
```
