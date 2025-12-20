using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Nodes;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ListBubblingNode 测试
/// </summary>
[Collection("Sequential")]
public class ListBubblingNodeTests
{
    [Fact]
    public void Add_TriggersNodeChanged()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
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
        var node = new ListBubblingNode<string>("TestList");
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
        Assert.Equal("Item2", receivedChange.Value.NewValue);
    }

    [Fact]
    public void RemoveAt_TriggersNodeChanged()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
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
        Assert.Equal("Item1", receivedChange.Value.OldValue);
    }

    [Fact]
    public void Remove_ByValue_TriggersNodeChanged()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
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
        var node = new ListBubblingNode<string>("TestList");
        node.Add("Item1");

        // Act
        var result = node.Remove("NonExistent");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Items_ReturnsCorrectSnapshot()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");
        node.Add("Item3");

        // Act
        var items = node.Items;

        // Assert
        Assert.Equal(3, items.Count);
        Assert.Equal("Item1", items[0]);
        Assert.Equal("Item2", items[1]);
        Assert.Equal("Item3", items[2]);
    }

    [Fact]
    public void Count_ReturnsCorrectValue()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");

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
        var node = new ListBubblingNode<string>("TestList");
        var eventCount = 0;
        node.NodeChanged += (sender, change) => eventCount++;

        // Act
        node.PopulateSilently(new[] { "Item1", "Item2", "Item3" });

        // Assert
        Assert.Equal(0, eventCount);
        Assert.Equal(3, node.Count);
    }

    [Fact]
    public void ListBubblingNode_BubblesToParent()
    {
        // Arrange
        var parent = new ListBubblingNode<string>("Parent");
        var child = new ListBubblingNode<int>("Child");
        parent.AttachChild(child);

        BubblingChange? receivedChange = null;
        parent.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        child.Add(42);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
    }

    [Fact]
    public void RemoveAt_InvalidIndex_ReturnsFalse()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
        node.Add("Item1");

        // Act & Assert
        Assert.False(node.RemoveAt(-1));
        Assert.False(node.RemoveAt(5));
    }

    [Fact]
    public void Items_Snapshot_IsImmutable()
    {
        // Arrange
        var node = new ListBubblingNode<string>("TestList");
        node.Add("Item1");
        node.Add("Item2");

        // Act
        var snapshot1 = node.Items;
        node.Add("Item3");
        var snapshot2 = node.Items;

        // Assert - 快照应该是不同的
        Assert.Equal(2, snapshot1.Count);
        Assert.Equal(3, snapshot2.Count);
    }
}
