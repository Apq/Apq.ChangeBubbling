using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Nodes.Concurrent;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ConcurrentBagBubblingNode 线程安全列表节点测试
/// </summary>
[Collection("Sequential")]
public class ConcurrentBagBubblingNodeTests
{
    [Fact]
    public void Add_TriggersNodeChanged()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Add("Item1");

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal("Item1", receivedChange.Value.NewValue);
    }

    [Fact]
    public void Insert_TriggersNodeChanged()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item3");

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Insert(1, "Item2");

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal(1, receivedChange.Value.Index);
    }

    [Fact]
    public void RemoveAt_TriggersNodeChanged()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        var result = node.RemoveAt(0);

        // Assert
        Assert.True(result);
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionRemove, receivedChange.Value.Kind);
    }

    [Fact]
    public void Remove_ByValue_TriggersNodeChanged()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        var result = node.Remove("Item1");

        // Assert
        Assert.True(result);
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionRemove, receivedChange.Value.Kind);
    }

    [Fact]
    public void Remove_NonExistent_ReturnsFalse()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");

        // Act
        var result = node.Remove("NonExistent");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Clear_TriggersCollectionReset()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Clear();

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionReset, receivedChange.Value.Kind);
        Assert.Equal(0, node.Count);
    }

    [Fact]
    public void Clear_EmptyList_DoesNotTriggerEvent()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        var eventCount = 0;
        node.NodeChanged += (sender, change) => eventCount++;

        // Act
        node.Clear();

        // Assert
        Assert.Equal(0, eventCount);
    }

    [Fact]
    public void Items_ReturnsCorrectSnapshot()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");
        node.Add("Item3");

        // Act
        var items = node.Items;

        // Assert
        Assert.Equal(3, items.Count);
    }

    [Fact]
    public void Count_ReturnsCorrectValue()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");

        // Act & Assert
        Assert.Equal(0, node.Count);

        node.Add("Item1");
        Assert.Equal(1, node.Count);

        node.Add("Item2");
        Assert.Equal(2, node.Count);

        node.RemoveAt(0);
        Assert.Equal(1, node.Count);
    }

    [Fact]
    public void PopulateSilently_DoesNotTriggerEvents()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        var eventCount = 0;
        node.NodeChanged += (sender, change) => eventCount++;

        // Act
        node.PopulateSilently(new[] { "Item1", "Item2", "Item3" });

        // Assert
        Assert.Equal(0, eventCount);
        Assert.Equal(3, node.Count);
    }

    [Fact]
    public void RemoveAt_InvalidIndex_ReturnsFalse()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<string>("TestList");
        node.Add("Item1");

        // Act & Assert
        Assert.False(node.RemoveAt(-1));
        Assert.False(node.RemoveAt(5));
    }

    [Fact]
    public async Task ConcurrentOperations_AreThreadSafe()
    {
        // Arrange
        var node = new ConcurrentBagBubblingNode<int>("TestList");
        var tasks = new List<Task>();
        var itemCount = 100;

        // Act - 并发添加
        for (int i = 0; i < itemCount; i++)
        {
            var value = i;
            tasks.Add(Task.Run(() => node.Add(value)));
        }
        await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(itemCount, node.Count);
    }
}
