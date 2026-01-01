# Dataflow Pipeline

Apq.ChangeBubbling provides TPL Dataflow-based backpressure pipelines for high-throughput scenarios.

## Basic Usage

```csharp
using Apq.ChangeBubbling.Infrastructure.Dataflow;

var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: 4,
    boundedCapacity: 1000
);

pipeline.AddStep(change =>
{
    Console.WriteLine($"Processing: {change.NodeName}");
    return change;
});

pipeline.AddAsyncStep(async change =>
{
    await SaveToDatabase(change);
    return change;
});

pipeline.Start();
await pipeline.SendAsync(change);

pipeline.Complete();
await pipeline.Completion;
```

## Configuration

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: Environment.ProcessorCount,
    boundedCapacity: 10000,
    cancellationToken: cts.Token
);
```

## Integration with Messaging

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: 4,
    boundedCapacity: 1000
);

pipeline.AddAsyncStep(async change =>
{
    await ProcessChangeAsync(change);
    return change;
});

pipeline.Start();

ChangeMessenger.AsObservable()
    .Subscribe(async change =>
    {
        await pipeline.SendAsync(change);
    });
```

## Best Practices

1. Set appropriate parallelism (usually CPU core count)
2. Set buffer capacity to prevent memory overflow
3. Use batch processing to reduce I/O operations
4. Handle errors to prevent pipeline from stopping
5. Monitor performance regularly
