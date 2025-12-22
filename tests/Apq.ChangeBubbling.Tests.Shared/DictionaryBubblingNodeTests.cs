using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Nodes;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// DictionaryBubblingNode 测试
/// </summary>
[Collection("Sequential")]
public class DictionaryBubblingNodeTests
{
    [Fact]
    public void Put_NewKey_TriggersNodeChanged()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Put("Key1", 100);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal("Key1", receivedChange.Value.Key);
        Assert.Equal(100, receivedChange.Value.NewValue);
    }

    [Fact]
    public void Put_ExistingKey_TriggersNodeChanged()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Put("Key1", 200);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal("Key1", receivedChange.Value.Key);
        Assert.Equal(200, receivedChange.Value.NewValue);
    }

    [Fact]
    public void Remove_TriggersNodeChanged()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        var result = node.Remove("Key1");

        // Assert
        Assert.True(result);
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionRemove, receivedChange.Value.Kind);
        Assert.Equal("Key1", receivedChange.Value.Key);
    }

    [Fact]
    public void Remove_NonExistent_ReturnsFalse()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");

        // Act
        var result = node.Remove("NonExistent");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryGet_ExistingKey_ReturnsTrue()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        // Act & Assert
        Assert.True(node.TryGet("Key1", out var value));
        Assert.Equal(100, value);
    }

    [Fact]
    public void TryGet_NonExistentKey_ReturnsFalse()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");

        // Act & Assert
        Assert.False(node.TryGet("Key2", out _));
    }

    [Fact]
    public void Count_ReturnsCorrectValue()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");

        // Act & Assert
        Assert.Equal(0, node.Count);

        node.Put("Key1", 100);
        Assert.Equal(1, node.Count);

        node.Put("Key2", 200);
        Assert.Equal(2, node.Count);

        node.Remove("Key1");
        Assert.Equal(1, node.Count);
    }

    [Fact]
    public void Keys_ReturnsAllKeys()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);
        node.Put("Key2", 200);

        // Act
        var keys = node.Keys;

        // Assert
        Assert.Equal(2, keys.Count);
        Assert.Contains("Key1", keys);
        Assert.Contains("Key2", keys);
    }

    [Fact]
    public void Items_ReturnsAllItems()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);
        node.Put("Key2", 200);

        // Act
        var items = node.Items;

        // Assert
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void DictionaryBubblingNode_BubblesToParent()
    {
        // Arrange
        var parent = new ListBubblingNode<string>("Parent");
        var child = new DictionaryBubblingNode<string, int>("Child");
        parent.AttachChild(child);

        BubblingChange? receivedChange = null;
        parent.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        child.Put("Key1", 42);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
    }

    [Fact]
    public void PopulateSilently_DoesNotTriggerEvents()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        var eventCount = 0;
        node.NodeChanged += (sender, change) => eventCount++;

        // Act
        node.PopulateSilently(new[]
        {
            new KeyValuePair<string, int>("Key1", 100),
            new KeyValuePair<string, int>("Key2", 200)
        });

        // Assert
        Assert.Equal(0, eventCount);
        Assert.Equal(2, node.Count);
    }

    [Fact]
    public void Keys_Snapshot_IsImmutable()
    {
        // Arrange
        var node = new DictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        // Act
        var snapshot1 = node.Keys;
        node.Put("Key2", 200);
        var snapshot2 = node.Keys;

        // Assert - 快照应该是不同的
        Assert.Single(snapshot1);
        Assert.Equal(2, snapshot2.Count);
    }
}
