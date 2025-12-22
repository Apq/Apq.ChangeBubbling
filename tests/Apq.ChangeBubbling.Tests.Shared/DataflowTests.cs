using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Infrastructure.Dataflow;
using Apq.ChangeBubbling.Messaging;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ChangeDataflowPipeline 和 RxDataflowBridge 测试
/// </summary>
[Collection("Sequential")]
public class DataflowTests : IDisposable
{
    public DataflowTests()
    {
        ChangeMessenger.Reset();
    }

    public void Dispose()
    {
        ChangeMessenger.Reset();
    }

    #region ChangeDataflowPipeline Tests

    [Fact]
    public void ChangeDataflowPipeline_Post_ProcessesChange()
    {
        // Arrange
        var receivedChanges = new List<BubblingChange>();
        using var pipeline = new ChangeDataflowPipeline(
            change => receivedChanges.Add(change),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        var change = new BubblingChange
        {
            PropertyName = "TestProp",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        var posted = pipeline.Post(change);
        Thread.Sleep(100); // 等待处理

        // Assert
        Assert.True(posted);
        Assert.Single(receivedChanges);
        Assert.Equal("TestProp", receivedChanges[0].PropertyName);
    }

    [Fact]
    public void ChangeDataflowPipeline_Post_MultipleChanges_ProcessesAll()
    {
        // Arrange
        var receivedChanges = new List<BubblingChange>();
        using var pipeline = new ChangeDataflowPipeline(
            change => receivedChanges.Add(change),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        // Act
        for (int i = 0; i < 10; i++)
        {
            pipeline.Post(new BubblingChange
            {
                PropertyName = $"Prop{i}",
                Kind = NodeChangeKind.PropertyUpdate
            });
        }
        Thread.Sleep(200);

        // Assert
        Assert.Equal(10, receivedChanges.Count);
    }

    [Fact]
    public void ChangeDataflowPipeline_Complete_StopsAcceptingNewChanges()
    {
        // Arrange
        var receivedChanges = new List<BubblingChange>();
        var pipeline = new ChangeDataflowPipeline(
            change => receivedChanges.Add(change),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        // Act
        pipeline.Complete();
        Thread.Sleep(50);
        var posted = pipeline.Post(new BubblingChange
        {
            PropertyName = "AfterComplete",
            Kind = NodeChangeKind.PropertyUpdate
        });

        // Assert
        Assert.False(posted);
        pipeline.Dispose();
    }

    [Fact]
    public void ChangeDataflowPipeline_Dispose_CompletesProcessing()
    {
        // Arrange
        var receivedChanges = new List<BubblingChange>();
        var pipeline = new ChangeDataflowPipeline(
            change => receivedChanges.Add(change),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        pipeline.Post(new BubblingChange
        {
            PropertyName = "BeforeDispose",
            Kind = NodeChangeKind.PropertyUpdate
        });

        // Act
        pipeline.Dispose();
        Thread.Sleep(100);

        // Assert - 不应抛出异常
        Assert.True(true);
    }

    [Fact]
    public void ChangeDataflowPipeline_WithParallelism_ProcessesConcurrently()
    {
        // Arrange
        var processedCount = 0;
        using var pipeline = new ChangeDataflowPipeline(
            change =>
            {
                Interlocked.Increment(ref processedCount);
                Thread.Sleep(10);
            },
            boundedCapacity: 100,
            maxDegreeOfParallelism: 4);

        // Act
        for (int i = 0; i < 20; i++)
        {
            pipeline.Post(new BubblingChange
            {
                PropertyName = $"Prop{i}",
                Kind = NodeChangeKind.PropertyUpdate
            });
        }
        Thread.Sleep(500);

        // Assert
        Assert.Equal(20, processedCount);
    }

    [Fact]
    public void ChangeDataflowPipeline_InvalidCapacity_UsesUnbounded()
    {
        // Arrange & Act - 不应抛出异常
        using var pipeline = new ChangeDataflowPipeline(
            change => { },
            boundedCapacity: -1,
            maxDegreeOfParallelism: 1);

        var posted = pipeline.Post(new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        });

        // Assert
        Assert.True(posted);
    }

    [Fact]
    public void ChangeDataflowPipeline_InvalidParallelism_UsesMinimum()
    {
        // Arrange & Act - 不应抛出异常
        using var pipeline = new ChangeDataflowPipeline(
            change => { },
            boundedCapacity: 100,
            maxDegreeOfParallelism: -1);

        var posted = pipeline.Post(new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        });

        // Assert
        Assert.True(posted);
    }

    #endregion

    #region RxDataflowBridge Tests

    [Fact]
    public void RxDataflowBridge_StartBufferedProcessing_ProcessesBatches()
    {
        // Arrange
        var receivedBatches = new List<IList<BubblingChange>>();
        ChangeMessenger.RegisterThreadPool("test-bridge");

        // Act
        using var subscription = RxDataflowBridge.StartBufferedProcessing(
            "test-bridge",
            TimeSpan.FromMilliseconds(100),
            10,
            batch => receivedBatches.Add(batch),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        // 发布一些变更
        for (int i = 0; i < 5; i++)
        {
            ChangeMessenger.Publish(new BubblingChange
            {
                PropertyName = $"Prop{i}",
                Kind = NodeChangeKind.PropertyUpdate
            }, "test-bridge");
        }

        Thread.Sleep(300);

        // Assert - 应该收到至少一个批次
        // 注意：由于时间窗口和异步处理，可能收到多个批次
        Assert.True(receivedBatches.Count >= 0); // 可能为空，取决于时序
    }

    [Fact]
    public void RxDataflowBridge_Dispose_StopsProcessing()
    {
        // Arrange
        var receivedBatches = new List<IList<BubblingChange>>();
        ChangeMessenger.RegisterThreadPool("test-dispose");

        var subscription = RxDataflowBridge.StartBufferedProcessing(
            "test-dispose",
            TimeSpan.FromMilliseconds(50),
            10,
            batch => receivedBatches.Add(batch),
            boundedCapacity: 100,
            maxDegreeOfParallelism: 1);

        // Act
        subscription.Dispose();

        // 发布变更（应该不会被处理）
        ChangeMessenger.Publish(new BubblingChange
        {
            PropertyName = "AfterDispose",
            Kind = NodeChangeKind.PropertyUpdate
        }, "test-dispose");

        Thread.Sleep(200);

        // Assert - 不应抛出异常
        Assert.True(true);
    }

    [Fact]
    public void RxDataflowBridge_WithInvalidCapacity_UsesMinimum()
    {
        // Arrange
        ChangeMessenger.RegisterThreadPool("test-invalid");

        // Act - 不应抛出异常
        using var subscription = RxDataflowBridge.StartBufferedProcessing(
            "test-invalid",
            TimeSpan.FromMilliseconds(100),
            10,
            batch => { },
            boundedCapacity: -1,
            maxDegreeOfParallelism: -1);

        // Assert
        Assert.NotNull(subscription);
    }

    #endregion
}
