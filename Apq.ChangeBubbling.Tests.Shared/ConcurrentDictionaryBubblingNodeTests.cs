using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Nodes.Concurrent;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ConcurrentDictionaryBubblingNode 线程安全字典节点测试
/// </summary>
public class ConcurrentDictionaryBubblingNodeTests
{
    [Fact]
    public void Put_NewKey_TriggersCollectionAdd()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
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
    public void Put_ExistingKey_TriggersCollectionReplace()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        BubblingChange? receivedChange = null;
        node.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        node.Put("Key1", 200);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionReplace, receivedChange.Value.Kind);
        Assert.Equal("Key1", receivedChange.Value.Key);
        Assert.Equal(100, receivedChange.Value.OldValue);
        Assert.Equal(200, receivedChange.Value.NewValue);
    }

    [Fact]
    public void Remove_TriggersNodeChanged()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
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
        Assert.Equal(100, receivedChange.Value.OldValue);
    }

    [Fact]
    public void Remove_NonExistent_ReturnsFalse()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");

        // Act
        var result = node.Remove("NonExistent");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryGet_ExistingKey_ReturnsTrue()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);

        // Act & Assert
        Assert.True(node.TryGet("Key1", out var value));
        Assert.Equal(100, value);
    }

    [Fact]
    public void TryGet_NonExistentKey_ReturnsFalse()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");

        // Act & Assert
        Assert.False(node.TryGet("Key2", out _));
    }

    [Fact]
    public void Count_ReturnsCorrectValue()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");

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
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
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
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
        node.Put("Key1", 100);
        node.Put("Key2", 200);

        // Act
        var items = node.Items;

        // Assert
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void PopulateSilently_DoesNotTriggerEvents()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<string, int>("TestDict");
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
    public async Task ConcurrentOperations_AreThreadSafe()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<int, int>("TestDict");
        var tasks = new List<Task>();
        var itemCount = 100;

        // Act - 并发添加
        for (int i = 0; i < itemCount; i++)
        {
            var key = i;
            tasks.Add(Task.Run(() => node.Put(key, key * 10)));
        }
        await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(itemCount, node.Count);
    }

    [Fact]
    public async Task ConcurrentPutAndRemove_AreThreadSafe()
    {
        // Arrange
        var node = new ConcurrentDictionaryBubblingNode<int, int>("TestDict");

        // 预填充一些数据
        for (int i = 0; i < 50; i++)
        {
            node.Put(i, i * 10);
        }

        var tasks = new List<Task>();

        // Act - 并发添加和删除
        for (int i = 0; i < 50; i++)
        {
            var key = i;
            tasks.Add(Task.Run(() => node.Put(key + 50, (key + 50) * 10)));
            tasks.Add(Task.Run(() => node.Remove(key)));
        }
        await Task.WhenAll(tasks);

        // Assert - 应该有 50 个新添加的项
        Assert.Equal(50, node.Count);
    }
}
