using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Messaging;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ChangeMessenger 消息中心测试
/// </summary>
public class ChangeMessengerTests : IDisposable
{
    public ChangeMessengerTests()
    {
        // 每个测试前重置消息中心
        ChangeMessenger.Reset();
    }

    public void Dispose()
    {
        // 每个测试后重置消息中心
        ChangeMessenger.Reset();
    }

    [Fact]
    public void Publish_ToDefaultEnv_Works()
    {
        // Arrange
        var receivedChanges = new List<BubblingChange>();
        using var subscription = ChangeMessenger.AsObservable()
            .Subscribe(change => receivedChanges.Add(change));

        var change = new BubblingChange
        {
            PropertyName = "TestProp",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        ChangeMessenger.Publish(change);

        // 等待异步处理
        Thread.Sleep(100);

        // Assert
        Assert.Single(receivedChanges);
        Assert.Equal("TestProp", receivedChanges[0].PropertyName);
    }

    [Fact]
    public void AsObservable_ReturnsObservableStream()
    {
        // Arrange & Act
        var observable = ChangeMessenger.AsObservable();

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void AsThrottledObservable_ReturnsThrottledStream()
    {
        // Arrange & Act
        var observable = ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100));

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void AsBufferedObservable_ReturnsBufferedStream()
    {
        // Arrange & Act
        var observable = ChangeMessenger.AsBufferedObservable(TimeSpan.FromMilliseconds(100), 10);

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void AsWindowedObservable_ReturnsWindowedStream()
    {
        // Arrange & Act
        var observable = ChangeMessenger.AsWindowedObservable(TimeSpan.FromMilliseconds(100));

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void RegisterThreadPool_CreatesEnvironment()
    {
        // Arrange & Act
        ChangeMessenger.RegisterThreadPool("test-env");
        var observable = ChangeMessenger.AsObservable("test-env");

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void RegisterDispatcher_CreatesEnvironment()
    {
        // Arrange & Act
        ChangeMessenger.RegisterDispatcher("dispatcher-env");
        var observable = ChangeMessenger.AsObservable("dispatcher-env");

        // Assert
        Assert.NotNull(observable);
    }

    [Fact]
    public void RegisterDedicatedThread_CreatesEnvironment()
    {
        // Arrange & Act
        using var disposable = ChangeMessenger.RegisterDedicatedThread("dedicated-env", "TestThread");
        var observable = ChangeMessenger.AsObservable("dedicated-env");

        // Assert
        Assert.NotNull(observable);
        Assert.NotNull(disposable);
    }

    [Fact]
    public void EnableMetrics_CanBeToggled()
    {
        // Arrange
        var original = ChangeMessenger.EnableMetrics;

        // Act
        ChangeMessenger.EnableMetrics = false;

        // Assert
        Assert.False(ChangeMessenger.EnableMetrics);

        // Cleanup
        ChangeMessenger.EnableMetrics = original;
    }

    [Fact]
    public void EnableWeakMessenger_CanBeToggled()
    {
        // Arrange
        var original = ChangeMessenger.EnableWeakMessenger;

        // Act
        ChangeMessenger.EnableWeakMessenger = false;

        // Assert
        Assert.False(ChangeMessenger.EnableWeakMessenger);

        // Cleanup
        ChangeMessenger.EnableWeakMessenger = original;
    }

    [Fact]
    public void EnableRxStream_CanBeToggled()
    {
        // Arrange
        var original = ChangeMessenger.EnableRxStream;

        // Act
        ChangeMessenger.EnableRxStream = false;

        // Assert
        Assert.False(ChangeMessenger.EnableRxStream);

        // Cleanup
        ChangeMessenger.EnableRxStream = original;
    }

    [Fact]
    public void UseSynchronousPublish_CanBeToggled()
    {
        // Arrange
        var original = ChangeMessenger.UseSynchronousPublish;

        // Act
        ChangeMessenger.UseSynchronousPublish = true;

        // Assert
        Assert.True(ChangeMessenger.UseSynchronousPublish);

        // Cleanup
        ChangeMessenger.UseSynchronousPublish = original;
    }

    [Fact]
    public void GetPerformanceMetrics_ReturnsMetrics()
    {
        // Arrange & Act
        var metrics = ChangeMessenger.GetPerformanceMetrics();

        // Assert
        Assert.NotNull(metrics);
    }

    [Fact]
    public void GetEventTypeStatistics_ReturnsStatistics()
    {
        // Arrange & Act
        var stats = ChangeMessenger.GetEventTypeStatistics();

        // Assert
        Assert.NotNull(stats);
    }

    [Fact]
    public void Reset_ClearsAllState()
    {
        // Arrange
        ChangeMessenger.RegisterThreadPool("custom-env");

        // Act
        ChangeMessenger.Reset();

        // Assert - 默认环境应该被重新注册
        var observable = ChangeMessenger.AsObservable();
        Assert.NotNull(observable);
    }

    [Fact]
    public void SynchronousPublish_WorksCorrectly()
    {
        // Arrange
        ChangeMessenger.UseSynchronousPublish = true;
        var receivedChanges = new List<BubblingChange>();
        using var subscription = ChangeMessenger.AsObservable()
            .Subscribe(change => receivedChanges.Add(change));

        var change = new BubblingChange
        {
            PropertyName = "SyncProp",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        ChangeMessenger.Publish(change);

        // 同步模式下应该立即收到
        Thread.Sleep(50);

        // Assert
        Assert.Single(receivedChanges);

        // Cleanup
        ChangeMessenger.UseSynchronousPublish = false;
    }
}
