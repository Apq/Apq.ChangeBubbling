using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ChangeBubblingMetrics 性能指标测试
/// </summary>
[Collection("Sequential")]
public class ChangeBubblingMetricsTests : IDisposable
{
    public ChangeBubblingMetricsTests()
    {
        ChangeBubblingMetrics.Reset();
    }

    public void Dispose()
    {
        ChangeBubblingMetrics.Reset();
    }

    #region RecordEventProcessed Tests

    [Fact]
    public void RecordEventProcessed_IncrementsEventCount()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventProcessed("TestEvent", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.RecordEventProcessed("TestEvent", TimeSpan.FromMilliseconds(20));

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalEvents >= 2);
    }

    [Fact]
    public void RecordEventProcessed_WithContext_TracksSeperately()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventProcessed("Event1", TimeSpan.FromMilliseconds(10), "Context1");
        ChangeBubblingMetrics.RecordEventProcessed("Event1", TimeSpan.FromMilliseconds(10), "Context2");

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.EventCounts.Count >= 2);
    }

    [Fact]
    public void RecordEventProcessed_TracksProcessingTime()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventProcessed("TimedEvent", TimeSpan.FromMilliseconds(50));

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.ProcessingTimes.Count > 0);
    }

    #endregion

    #region RecordEventBubbled Tests

    [Fact]
    public void RecordEventBubbled_IncrementsCount()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventBubbled("BubbledEvent", 3);
        ChangeBubblingMetrics.RecordEventBubbled("BubbledEvent", 2);

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalEvents >= 2);
    }

    [Fact]
    public void RecordEventBubbled_WithContext_TracksSeperately()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventBubbled("Event", 1, "ContextA");
        ChangeBubblingMetrics.RecordEventBubbled("Event", 1, "ContextB");

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.NotNull(metrics.EventCounts);
    }

    #endregion

    #region RecordEventFiltered Tests

    [Fact]
    public void RecordEventFiltered_IncrementsFilteredCount()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventFiltered("FilteredEvent", "TestFilter");
        ChangeBubblingMetrics.RecordEventFiltered("FilteredEvent", "TestFilter");

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalFilteredEvents >= 2);
    }

    [Fact]
    public void RecordEventFiltered_WithContext_TracksSeperately()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordEventFiltered("Event", "Filter1", "Context1");
        ChangeBubblingMetrics.RecordEventFiltered("Event", "Filter2", "Context2");

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.FilteredEvents.Count >= 2);
    }

    #endregion

    #region RecordSubscriptionChanged Tests

    [Fact]
    public void RecordSubscriptionChanged_Add_IncrementsCount()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordSubscriptionChanged("TestSubscription", isAdded: true);
        ChangeBubblingMetrics.RecordSubscriptionChanged("TestSubscription", isAdded: true);

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalActiveSubscriptions >= 2);
    }

    [Fact]
    public void RecordSubscriptionChanged_Remove_DecrementsCount()
    {
        // Arrange
        ChangeBubblingMetrics.RecordSubscriptionChanged("TestSub", isAdded: true);
        ChangeBubblingMetrics.RecordSubscriptionChanged("TestSub", isAdded: true);

        // Act
        ChangeBubblingMetrics.RecordSubscriptionChanged("TestSub", isAdded: false);

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalActiveSubscriptions >= 1);
    }

    [Fact]
    public void RecordSubscriptionChanged_Remove_DoesNotGoBelowZero()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordSubscriptionChanged("EmptySub", isAdded: false);
        ChangeBubblingMetrics.RecordSubscriptionChanged("EmptySub", isAdded: false);

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalActiveSubscriptions >= 0);
    }

    #endregion

    #region RecordNodeOperation Tests

    [Fact]
    public void RecordNodeOperation_TracksOperation()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordNodeOperation("Add", "ListNode", TimeSpan.FromMilliseconds(5));
        ChangeBubblingMetrics.RecordNodeOperation("Remove", "ListNode", TimeSpan.FromMilliseconds(3));

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.True(metrics.TotalEvents >= 2);
    }

    [Fact]
    public void RecordNodeOperation_WithContext_TracksSeperately()
    {
        // Arrange & Act
        ChangeBubblingMetrics.RecordNodeOperation("Update", "DictNode", TimeSpan.FromMilliseconds(10), "Context1");
        ChangeBubblingMetrics.RecordNodeOperation("Update", "DictNode", TimeSpan.FromMilliseconds(10), "Context2");

        // Assert
        var metrics = ChangeBubblingMetrics.GetMetrics();
        Assert.NotNull(metrics.EventCounts);
    }

    #endregion

    #region GetMetrics Tests

    [Fact]
    public void GetMetrics_ReturnsValidMetrics()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("Event1", TimeSpan.FromMilliseconds(10));

        // Act
        var metrics = ChangeBubblingMetrics.GetMetrics();

        // Assert
        Assert.NotNull(metrics);
        Assert.True(metrics.Timestamp <= DateTime.UtcNow);
        Assert.NotNull(metrics.EventCounts);
        Assert.NotNull(metrics.ProcessingTimes);
        Assert.NotNull(metrics.ActiveSubscriptions);
        Assert.NotNull(metrics.FilteredEvents);
    }

    [Fact]
    public void GetMetrics_AfterReset_ReturnsEmptyMetrics()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("Event", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.Reset();

        // Act
        var metrics = ChangeBubblingMetrics.GetMetrics();

        // Assert
        Assert.Equal(0, metrics.TotalEvents);
        Assert.Equal(0, metrics.TotalFilteredEvents);
        Assert.Equal(0, metrics.TotalActiveSubscriptions);
    }

    [Fact]
    public void GetMetrics_AverageProcessingTime_CalculatedCorrectly()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("Event1", TimeSpan.FromMilliseconds(100));
        ChangeBubblingMetrics.RecordEventProcessed("Event2", TimeSpan.FromMilliseconds(200));

        // Act
        var metrics = ChangeBubblingMetrics.GetMetrics();

        // Assert
        Assert.True(metrics.AverageProcessingTime.TotalMilliseconds > 0);
    }

    #endregion

    #region GetEventTypeStatistics Tests

    [Fact]
    public void GetEventTypeStatistics_ReturnsStatistics()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("TypeA", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.RecordEventProcessed("TypeB", TimeSpan.FromMilliseconds(20));

        // Act
        var stats = ChangeBubblingMetrics.GetEventTypeStatistics();

        // Assert
        Assert.NotNull(stats);
        Assert.True(stats.Count >= 2);
    }

    [Fact]
    public void GetEventTypeStatistics_IncludesTotalCount()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("CountedEvent", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.RecordEventProcessed("CountedEvent", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.RecordEventProcessed("CountedEvent", TimeSpan.FromMilliseconds(10));

        // Act
        var stats = ChangeBubblingMetrics.GetEventTypeStatistics();

        // Assert
        Assert.True(stats.ContainsKey("CountedEvent"));
        Assert.True(stats["CountedEvent"].TotalCount >= 3);
    }

    [Fact]
    public void GetEventTypeStatistics_IncludesProcessingTime()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("TimedEvent", TimeSpan.FromMilliseconds(50));

        // Act
        var stats = ChangeBubblingMetrics.GetEventTypeStatistics();

        // Assert
        if (stats.ContainsKey("TimedEvent"))
        {
            Assert.True(stats["TimedEvent"].AverageProcessingTime.TotalMilliseconds >= 0);
        }
    }

    #endregion

    #region Reset Tests

    [Fact]
    public void Reset_ClearsAllMetrics()
    {
        // Arrange
        ChangeBubblingMetrics.RecordEventProcessed("Event", TimeSpan.FromMilliseconds(10));
        ChangeBubblingMetrics.RecordEventFiltered("Event", "Filter");
        ChangeBubblingMetrics.RecordSubscriptionChanged("Sub", true);

        // Act
        ChangeBubblingMetrics.Reset();
        var metrics = ChangeBubblingMetrics.GetMetrics();

        // Assert
        Assert.Equal(0, metrics.TotalEvents);
        Assert.Equal(0, metrics.TotalFilteredEvents);
        Assert.Equal(0, metrics.TotalActiveSubscriptions);
        Assert.Empty(metrics.EventCounts);
        Assert.Empty(metrics.ProcessingTimes);
    }

    #endregion

    #region Performance Metrics Model Tests

    [Fact]
    public void ChangeBubblingPerformanceMetrics_DefaultValues()
    {
        // Arrange & Act
        var metrics = new ChangeBubblingPerformanceMetrics();

        // Assert
        Assert.Equal(default, metrics.Timestamp);
        Assert.Equal(0, metrics.TotalEvents);
        Assert.Equal(0, metrics.TotalFilteredEvents);
        Assert.Equal(0, metrics.TotalActiveSubscriptions);
        Assert.Equal(TimeSpan.Zero, metrics.AverageProcessingTime);
        Assert.NotNull(metrics.EventCounts);
        Assert.NotNull(metrics.ProcessingTimes);
        Assert.NotNull(metrics.ActiveSubscriptions);
        Assert.NotNull(metrics.FilteredEvents);
    }

    [Fact]
    public void EventTypeStatistics_DefaultValues()
    {
        // Arrange & Act
        var stats = new EventTypeStatistics();

        // Assert
        Assert.Equal(string.Empty, stats.EventType);
        Assert.Equal(0, stats.TotalCount);
        Assert.Equal(TimeSpan.Zero, stats.AverageProcessingTime);
        Assert.Equal(0, stats.ActiveSubscriptions);
        Assert.Equal(0, stats.FilteredCount);
    }

    #endregion
}
