using System.Collections;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Collections;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ObservableCollectionAdapter 和 CollectionAdapters 测试
/// </summary>
[Collection("Sequential")]
public class ObservableCollectionAdapterTests
{
    #region ObservableCollectionAdapter Basic Tests

    [Fact]
    public void Constructor_WithList_CreatesAdapter()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };

        // Act
        var adapter = new ObservableCollectionAdapter(list, "TestList");

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("TestList", adapter.Name);
        Assert.Equal(3, adapter.Count);
    }

    [Fact]
    public void Constructor_WithDictionary_CreatesAdapter()
    {
        // Arrange
        var dict = new Dictionary<string, int>
        {
            ["A"] = 1,
            ["B"] = 2
        };

        // Act
        var adapter = new ObservableCollectionAdapter(dict, "TestDict");

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("TestDict", adapter.Name);
        Assert.Equal(2, adapter.Count);
    }

    [Fact]
    public void Constructor_NullCollection_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ObservableCollectionAdapter(null!, "Test"));
    }

    [Fact]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        // Arrange
        var list = new List<int>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ObservableCollectionAdapter(list, null!));
    }

    [Fact]
    public void Proxied_ReturnsProxyObject()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");

        // Act
        var proxied = adapter.Proxied;

        // Assert
        Assert.NotNull(proxied);
    }

    [Fact]
    public void Items_ReturnsAllItems()
    {
        // Arrange
        var list = new List<string> { "A", "B", "C" };
        var adapter = new ObservableCollectionAdapter(list, "TestList");

        // Act
        var items = adapter.Items.ToList();

        // Assert
        Assert.Equal(3, items.Count);
        Assert.Contains("A", items);
        Assert.Contains("B", items);
        Assert.Contains("C", items);
    }

    [Fact]
    public void GetEnumerator_EnumeratesItems()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");

        // Act
        var items = new List<object?>();
        foreach (var item in adapter)
        {
            items.Add(item);
        }

        // Assert
        Assert.Equal(3, items.Count);
    }

    #endregion

    #region NodeChanged Event Tests

    [Fact]
    public void ProxiedList_Add_TriggersNodeChanged()
    {
        // Arrange
        var list = new List<int>();
        var adapter = new ObservableCollectionAdapter(list, "TestList");
        var proxied = (IList<int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Add(42);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal("TestList", receivedChange.Value.PropertyName);
    }

    [Fact]
    public void ProxiedList_Remove_TriggersNodeChanged()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");
        var proxied = (IList<int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Remove(2);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionRemove, receivedChange.Value.Kind);
    }

    [Fact]
    public void ProxiedList_Insert_TriggersNodeChanged()
    {
        // Arrange
        var list = new List<int> { 1, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");
        var proxied = (IList<int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Insert(1, 2);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal(1, receivedChange.Value.Index);
    }

    [Fact]
    public void ProxiedList_Clear_TriggersNodeChanged()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");
        var proxied = (IList<int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Clear();

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionReset, receivedChange.Value.Kind);
    }

    [Fact]
    public void ProxiedList_SetItem_TriggersNodeChanged()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var adapter = new ObservableCollectionAdapter(list, "TestList");
        var proxied = (IList<int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied[1] = 99;

        // Assert
        Assert.NotNull(receivedChange);
        // 注意：由于 existedBefore 检查只针对字典，List 的 set_Item 返回 CollectionAdd
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal(2, receivedChange.Value.OldValue);
        Assert.Equal(99, receivedChange.Value.NewValue);
    }

    [Fact]
    public void ProxiedDict_Add_TriggersNodeChanged()
    {
        // Arrange
        var dict = new Dictionary<string, int>();
        var adapter = new ObservableCollectionAdapter(dict, "TestDict");
        var proxied = (IDictionary<string, int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Add("Key1", 100);

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
        Assert.Equal("Key1", receivedChange.Value.Key);
    }

    [Fact]
    public void ProxiedDict_Remove_TriggersNodeChanged()
    {
        // Arrange
        var dict = new Dictionary<string, int> { ["Key1"] = 100 };
        var adapter = new ObservableCollectionAdapter(dict, "TestDict");
        var proxied = (IDictionary<string, int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied.Remove("Key1");

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionRemove, receivedChange.Value.Kind);
        Assert.Equal("Key1", receivedChange.Value.Key);
    }

    [Fact]
    public void ProxiedDict_SetItem_NewKey_TriggersCollectionAdd()
    {
        // Arrange
        var dict = new Dictionary<string, int>();
        var adapter = new ObservableCollectionAdapter(dict, "TestDict");
        var proxied = (IDictionary<string, int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied["NewKey"] = 200;

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionAdd, receivedChange.Value.Kind);
    }

    [Fact]
    public void ProxiedDict_SetItem_ExistingKey_TriggersCollectionReplace()
    {
        // Arrange
        var dict = new Dictionary<string, int> { ["Key1"] = 100 };
        var adapter = new ObservableCollectionAdapter(dict, "TestDict");
        var proxied = (IDictionary<string, int>)adapter.Proxied;

        BubblingChange? receivedChange = null;
        adapter.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        proxied["Key1"] = 200;

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal(NodeChangeKind.CollectionReplace, receivedChange.Value.Kind);
        Assert.Equal(100, receivedChange.Value.OldValue);
        Assert.Equal(200, receivedChange.Value.NewValue);
    }

    #endregion

    #region CollectionAdapters Factory Tests

    [Fact]
    public void CollectionAdapters_Create_ReturnsAdapter()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };

        // Act
        var adapter = CollectionAdapters.Create(list, "FactoryList");

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("FactoryList", adapter.Name);
        Assert.Equal(3, adapter.Count);
    }

    [Fact]
    public void CollectionAdapters_Create_WithDictionary_ReturnsAdapter()
    {
        // Arrange
        var dict = new Dictionary<string, string>
        {
            ["Key1"] = "Value1"
        };

        // Act
        var adapter = CollectionAdapters.Create(dict, "FactoryDict");

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("FactoryDict", adapter.Name);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Adapter_WithEmptyList_WorksCorrectly()
    {
        // Arrange
        var list = new List<int>();
        var adapter = new ObservableCollectionAdapter(list, "EmptyList");

        // Assert
        Assert.Equal(0, adapter.Count);
        Assert.Empty(adapter.Items);
    }

    [Fact]
    public void Adapter_WithArrayList_WorksCorrectly()
    {
        // Arrange
        var list = new ArrayList { 1, "Two", 3.0 };
        var adapter = new ObservableCollectionAdapter(list, "ArrayList");

        // Assert
        Assert.Equal(3, adapter.Count);
    }

    [Fact]
    public void Adapter_WithHashtable_WorksCorrectly()
    {
        // Arrange
        var table = new Hashtable
        {
            ["Key1"] = "Value1",
            ["Key2"] = "Value2"
        };
        var adapter = new ObservableCollectionAdapter(table, "Hashtable");

        // Assert
        Assert.Equal(2, adapter.Count);
    }

    #endregion
}
