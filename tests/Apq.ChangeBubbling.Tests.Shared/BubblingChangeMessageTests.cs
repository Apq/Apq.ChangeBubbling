using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Messaging;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// BubblingChangeMessage 消息池化测试
/// </summary>
[Collection("Sequential")]
public class BubblingChangeMessageTests
{
    #region Rent Tests

    [Fact]
    public void Rent_ReturnsMessage()
    {
        // Arrange
        var change = new BubblingChange
        {
            PropertyName = "TestProp",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        var message = BubblingChangeMessage.Rent(change);

        // Assert
        Assert.NotNull(message);
        message.Return();
    }

    [Fact]
    public void Rent_SetsValueCorrectly()
    {
        // Arrange
        var change = new BubblingChange
        {
            PropertyName = "TestProp",
            Kind = NodeChangeKind.PropertyUpdate,
            NewValue = 42
        };

        // Act
        var message = BubblingChangeMessage.Rent(change);

        // Assert
        Assert.Equal("TestProp", message.Value.PropertyName);
        Assert.Equal(NodeChangeKind.PropertyUpdate, message.Value.Kind);
        Assert.Equal(42, message.Value.NewValue);
        message.Return();
    }

    [Fact]
    public void Rent_MultipleMessages_ReturnsDistinctInstances()
    {
        // Arrange
        var change1 = new BubblingChange { PropertyName = "Prop1", Kind = NodeChangeKind.PropertyUpdate };
        var change2 = new BubblingChange { PropertyName = "Prop2", Kind = NodeChangeKind.PropertyUpdate };

        // Act
        var message1 = BubblingChangeMessage.Rent(change1);
        var message2 = BubblingChangeMessage.Rent(change2);

        // Assert
        Assert.NotSame(message1, message2);
        Assert.Equal("Prop1", message1.Value.PropertyName);
        Assert.Equal("Prop2", message2.Value.PropertyName);

        message1.Return();
        message2.Return();
    }

    #endregion
    #region Return Tests

    [Fact]
    public void Return_ReturnsMessageToPool()
    {
        // Arrange
        var change = new BubblingChange { PropertyName = "TestProp", Kind = NodeChangeKind.PropertyUpdate };
        var message = BubblingChangeMessage.Rent(change);

        // Act & Assert - 不应抛出异常
        message.Return();
    }

    [Fact]
    public void Return_AllowsReuse()
    {
        // Arrange
        var change1 = new BubblingChange { PropertyName = "Prop1", Kind = NodeChangeKind.PropertyUpdate };
        var message1 = BubblingChangeMessage.Rent(change1);
        message1.Return();

        // Act - 从池中获取（可能是同一个实例）
        var change2 = new BubblingChange { PropertyName = "Prop2", Kind = NodeChangeKind.PropertyUpdate };
        var message2 = BubblingChangeMessage.Rent(change2);

        // Assert
        Assert.NotNull(message2);
        Assert.Equal("Prop2", message2.Value.PropertyName);
        message2.Return();
    }

    #endregion

    #region Pool Behavior Tests

    [Fact]
    public void Pool_HighFrequencyUsage_WorksCorrectly()
    {
        // Arrange & Act
        var messages = new List<BubblingChangeMessage>();

        for (int i = 0; i < 100; i++)
        {
            var change = new BubblingChange
            {
                PropertyName = $"Prop{i}",
                Kind = NodeChangeKind.PropertyUpdate
            };
            messages.Add(BubblingChangeMessage.Rent(change));
        }

        // Assert
        Assert.Equal(100, messages.Count);

        // Return all
        foreach (var msg in messages)
        {
            msg.Return();
        }
    }

    [Fact]
    public void Pool_ConcurrentUsage_WorksCorrectly()
    {
        // Arrange
        var tasks = new List<Task>();
        var counter = 0;

        // Act
        for (int i = 0; i < 50; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                var change = new BubblingChange
                {
                    PropertyName = "ConcurrentProp",
                    Kind = NodeChangeKind.PropertyUpdate
                };
                var message = BubblingChangeMessage.Rent(change);
                Interlocked.Increment(ref counter);
                Thread.Sleep(1);
                message.Return();
            }));
        }

        Task.WaitAll(tasks.ToArray());

        // Assert
        Assert.Equal(50, counter);
    }
    [Fact]
    public void Pool_RentReturnCycle_MaintainsCorrectValues()
    {
        // Arrange & Act
        for (int cycle = 0; cycle < 10; cycle++)
        {
            var change = new BubblingChange
            {
                PropertyName = $"Cycle{cycle}",
                Kind = NodeChangeKind.CollectionAdd,
                Index = cycle
            };

            var message = BubblingChangeMessage.Rent(change);

            // Assert
            Assert.Equal($"Cycle{cycle}", message.Value.PropertyName);
            Assert.Equal(NodeChangeKind.CollectionAdd, message.Value.Kind);
            Assert.Equal(cycle, message.Value.Index);

            message.Return();
        }
    }

    #endregion

    #region Value Property Tests

    [Fact]
    public void Value_ContainsAllChangeProperties()
    {
        // Arrange
        var change = new BubblingChange
        {
            PropertyName = "FullProp",
            Kind = NodeChangeKind.CollectionReplace,
            OldValue = "old",
            NewValue = "new",
            Index = 5,
            Key = "testKey"
        };

        // Act
        var message = BubblingChangeMessage.Rent(change);

        // Assert
        Assert.Equal("FullProp", message.Value.PropertyName);
        Assert.Equal(NodeChangeKind.CollectionReplace, message.Value.Kind);
        Assert.Equal("old", message.Value.OldValue);
        Assert.Equal("new", message.Value.NewValue);
        Assert.Equal(5, message.Value.Index);
        Assert.Equal("testKey", message.Value.Key);

        message.Return();
    }

    [Fact]
    public void Value_WithPathSegments_PreservesPath()
    {
        // Arrange
        var change = new BubblingChange
        {
            PropertyName = "PathProp",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child", "Leaf" }
        };

        // Act
        var message = BubblingChangeMessage.Rent(change);

        // Assert
        Assert.NotNull(message.Value.PathSegments);
        Assert.Equal(3, message.Value.PathSegments.Count);
        Assert.Equal("Root", message.Value.PathSegments[0]);
        Assert.Equal("Child", message.Value.PathSegments[1]);
        Assert.Equal("Leaf", message.Value.PathSegments[2]);

        message.Return();
    }

    #endregion

    #region Inheritance Tests

    [Fact]
    public void Message_InheritsFromValueChangedMessage()
    {
        // Arrange
        var change = new BubblingChange { PropertyName = "InheritTest", Kind = NodeChangeKind.PropertyUpdate };

        // Act
        var message = BubblingChangeMessage.Rent(change);

        // Assert
        Assert.IsAssignableFrom<CommunityToolkit.Mvvm.Messaging.Messages.ValueChangedMessage<BubblingChange>>(message);

        message.Return();
    }

    #endregion
}