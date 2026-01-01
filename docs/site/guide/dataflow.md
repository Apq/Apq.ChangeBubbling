# 背压管线

Apq.ChangeBubbling 提供基于 TPL Dataflow 的背压管线，用于处理高吞吐量场景。

## 概述

背压（Backpressure）是一种流量控制机制，当消费者处理速度跟不上生产者时，可以通知生产者减速或缓冲数据。

## 基本用法

```csharp
using Apq.ChangeBubbling.Infrastructure.Dataflow;

// 创建背压管线
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: 4,
    boundedCapacity: 1000
);

// 添加处理步骤
pipeline.AddStep(change =>
{
    Console.WriteLine($"Processing: {change.NodeName}");
    return change;
});

// 添加异步处理步骤
pipeline.AddAsyncStep(async change =>
{
    await SaveToDatabase(change);
    return change;
});

// 启动管线
pipeline.Start();

// 发送数据
await pipeline.SendAsync(change);

// 完成并等待
pipeline.Complete();
await pipeline.Completion;
```

## 配置选项

### 并行度

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: Environment.ProcessorCount
);
```

### 缓冲容量

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    boundedCapacity: 10000  // 最大缓冲10000个元素
);
```

### 取消令牌

```csharp
var cts = new CancellationTokenSource();

var pipeline = new DataflowPipeline<BubblingChange>(
    cancellationToken: cts.Token
);

// 取消管线
cts.Cancel();
```

## 与消息系统集成

```csharp
// 创建管线
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

// 订阅消息并发送到管线
ChangeMessenger.AsObservable()
    .Subscribe(async change =>
    {
        await pipeline.SendAsync(change);
    });
```

## 批处理管线

```csharp
var batchPipeline = new BatchDataflowPipeline<BubblingChange>(
    batchSize: 100,
    maxWaitTime: TimeSpan.FromSeconds(1)
);

batchPipeline.AddBatchStep(batch =>
{
    Console.WriteLine($"Processing batch of {batch.Count} items");
    foreach (var change in batch)
    {
        Process(change);
    }
    return batch;
});

batchPipeline.Start();
```

## 错误处理

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: 4
);

pipeline.AddStep(change =>
{
    try
    {
        return ProcessChange(change);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return change; // 或返回 null 跳过
    }
});

// 监控完成状态
pipeline.Completion.ContinueWith(t =>
{
    if (t.IsFaulted)
    {
        Console.WriteLine($"Pipeline failed: {t.Exception}");
    }
});
```

## 性能监控

```csharp
var pipeline = new DataflowPipeline<BubblingChange>(
    maxDegreeOfParallelism: 4,
    boundedCapacity: 1000
);

// 获取管线状态
var inputCount = pipeline.InputCount;
var outputCount = pipeline.OutputCount;

Console.WriteLine($"Pending: {inputCount}, Processed: {outputCount}");
```

## 最佳实践

1. **设置合适的并行度**：通常设置为 CPU 核心数
2. **设置缓冲容量**：防止内存溢出
3. **使用批处理**：减少 I/O 操作次数
4. **处理错误**：避免管线因异常而停止
5. **监控性能**：定期检查管线状态
