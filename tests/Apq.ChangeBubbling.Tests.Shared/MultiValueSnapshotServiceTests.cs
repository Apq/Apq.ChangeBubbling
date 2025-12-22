using System.Collections;
using Apq.ChangeBubbling.Snapshot;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// MultiValueSnapshotService 多值容器快照服务测试
/// </summary>
[Collection("Sequential")]
public class MultiValueSnapshotServiceTests
{
    #region ExportFromList Tests

    [Fact]
    public void ExportFromList_EmptyList_ReturnsEmptySnapshot()
    {
        // Arrange
        var list = new List<int>();

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("TestList", list);

        // Assert
        Assert.NotNull(snapshot);
        Assert.Equal("TestList", snapshot.Name);
        Assert.Equal("List", snapshot.Kind);
        Assert.Empty(snapshot.ListItems);
    }

    [Fact]
    public void ExportFromList_WithItems_ReturnsAllItems()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("NumberList", list);

        // Assert
        Assert.Equal("NumberList", snapshot.Name);
        Assert.Equal("List", snapshot.Kind);
        Assert.Equal(5, snapshot.ListItems.Count);
        Assert.Equal(1, snapshot.ListItems[0]);
        Assert.Equal(5, snapshot.ListItems[4]);
    }

    [Fact]
    public void ExportFromList_WithStringItems_ReturnsCorrectly()
    {
        // Arrange
        var list = new List<string> { "Apple", "Banana", "Cherry" };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("FruitList", list);

        // Assert
        Assert.Equal("FruitList", snapshot.Name);
        Assert.Equal(3, snapshot.ListItems.Count);
        Assert.Equal("Apple", snapshot.ListItems[0]);
        Assert.Equal("Banana", snapshot.ListItems[1]);
        Assert.Equal("Cherry", snapshot.ListItems[2]);
    }

    [Fact]
    public void ExportFromList_WithNullItems_PreservesNulls()
    {
        // Arrange
        var list = new List<string?> { "First", null, "Third" };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("NullableList", list);

        // Assert
        Assert.Equal(3, snapshot.ListItems.Count);
        Assert.Equal("First", snapshot.ListItems[0]);
        Assert.Null(snapshot.ListItems[1]);
        Assert.Equal("Third", snapshot.ListItems[2]);
    }

    [Fact]
    public void ExportFromList_WithMixedTypes_ReturnsAllItems()
    {
        // Arrange
        var list = new ArrayList { 1, "Two", 3.0, true };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("MixedList", list);

        // Assert
        Assert.Equal(4, snapshot.ListItems.Count);
        Assert.Equal(1, snapshot.ListItems[0]);
        Assert.Equal("Two", snapshot.ListItems[1]);
        Assert.Equal(3.0, snapshot.ListItems[2]);
        Assert.Equal(true, snapshot.ListItems[3]);
    }

    [Fact]
    public void ExportFromList_Array_ReturnsCorrectly()
    {
        // Arrange
        var array = new int[] { 10, 20, 30 };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("ArrayList", array);

        // Assert
        Assert.Equal("ArrayList", snapshot.Name);
        Assert.Equal("List", snapshot.Kind);
        Assert.Equal(3, snapshot.ListItems.Count);
    }

    #endregion

    #region ExportFromDictionary Tests

    [Fact]
    public void ExportFromDictionary_EmptyDictionary_ReturnsEmptySnapshot()
    {
        // Arrange
        var dict = new Hashtable();

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("TestDict", dict);

        // Assert
        Assert.NotNull(snapshot);
        Assert.Equal("TestDict", snapshot.Name);
        Assert.Equal("Dictionary", snapshot.Kind);
        Assert.Empty(snapshot.DictItems);
    }

    [Fact]
    public void ExportFromDictionary_WithItems_ReturnsAllItems()
    {
        // Arrange
        var dict = new Hashtable
        {
            ["Key1"] = "Value1",
            ["Key2"] = "Value2",
            ["Key3"] = "Value3"
        };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("StringDict", dict);

        // Assert
        Assert.Equal("StringDict", snapshot.Name);
        Assert.Equal("Dictionary", snapshot.Kind);
        Assert.Equal(3, snapshot.DictItems.Count);
        Assert.Equal("Value1", snapshot.DictItems["Key1"]);
        Assert.Equal("Value2", snapshot.DictItems["Key2"]);
        Assert.Equal("Value3", snapshot.DictItems["Key3"]);
    }

    [Fact]
    public void ExportFromDictionary_WithIntKeys_ConvertsToString()
    {
        // Arrange
        var dict = new Hashtable
        {
            [1] = "One",
            [2] = "Two",
            [3] = "Three"
        };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("IntKeyDict", dict);

        // Assert
        Assert.Equal(3, snapshot.DictItems.Count);
        Assert.Equal("One", snapshot.DictItems["1"]);
        Assert.Equal("Two", snapshot.DictItems["2"]);
        Assert.Equal("Three", snapshot.DictItems["3"]);
    }

    [Fact]
    public void ExportFromDictionary_WithNullValues_PreservesNulls()
    {
        // Arrange
        var dict = new Hashtable
        {
            ["Key1"] = "Value1",
            ["Key2"] = null,
            ["Key3"] = "Value3"
        };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("NullValueDict", dict);

        // Assert
        Assert.Equal(3, snapshot.DictItems.Count);
        Assert.Equal("Value1", snapshot.DictItems["Key1"]);
        Assert.Null(snapshot.DictItems["Key2"]);
        Assert.Equal("Value3", snapshot.DictItems["Key3"]);
    }

    [Fact]
    public void ExportFromDictionary_WithMixedValueTypes_ReturnsAllItems()
    {
        // Arrange
        var dict = new Hashtable
        {
            ["Int"] = 42,
            ["String"] = "Hello",
            ["Double"] = 3.14,
            ["Bool"] = true
        };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("MixedDict", dict);

        // Assert
        Assert.Equal(4, snapshot.DictItems.Count);
        Assert.Equal(42, snapshot.DictItems["Int"]);
        Assert.Equal("Hello", snapshot.DictItems["String"]);
        Assert.Equal(3.14, snapshot.DictItems["Double"]);
        Assert.Equal(true, snapshot.DictItems["Bool"]);
    }

    [Fact]
    public void ExportFromDictionary_GenericDictionary_ReturnsCorrectly()
    {
        // Arrange
        IDictionary dict = new Dictionary<string, int>
        {
            ["A"] = 1,
            ["B"] = 2,
            ["C"] = 3
        };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("GenericDict", dict);

        // Assert
        Assert.Equal("GenericDict", snapshot.Name);
        Assert.Equal("Dictionary", snapshot.Kind);
        Assert.Equal(3, snapshot.DictItems.Count);
        Assert.Equal(1, snapshot.DictItems["A"]);
        Assert.Equal(2, snapshot.DictItems["B"]);
        Assert.Equal(3, snapshot.DictItems["C"]);
    }

    #endregion

    #region Snapshot Model Tests

    [Fact]
    public void MultiValueSnapshot_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var snapshot = new MultiValueSnapshot();

        // Assert
        Assert.Equal(string.Empty, snapshot.Name);
        Assert.Equal(string.Empty, snapshot.Kind);
        Assert.NotNull(snapshot.ListItems);
        Assert.Empty(snapshot.ListItems);
        Assert.NotNull(snapshot.DictItems);
        Assert.Empty(snapshot.DictItems);
    }

    [Fact]
    public void MultiValueSnapshot_CanSetProperties()
    {
        // Arrange & Act
        var snapshot = new MultiValueSnapshot
        {
            Name = "TestSnapshot",
            Kind = "List"
        };
        snapshot.ListItems.Add(1);
        snapshot.ListItems.Add(2);

        // Assert
        Assert.Equal("TestSnapshot", snapshot.Name);
        Assert.Equal("List", snapshot.Kind);
        Assert.Equal(2, snapshot.ListItems.Count);
    }

    [Fact]
    public void NodeSnapshot_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var snapshot = new NodeSnapshot();

        // Assert
        Assert.Equal(string.Empty, snapshot.Name);
        Assert.NotNull(snapshot.Properties);
        Assert.Empty(snapshot.Properties);
        Assert.NotNull(snapshot.Children);
        Assert.Empty(snapshot.Children);
    }

    [Fact]
    public void NodeSnapshot_CanSetProperties()
    {
        // Arrange & Act
        var snapshot = new NodeSnapshot
        {
            Name = "TestNode"
        };
        snapshot.Properties["Key"] = "Value";
        snapshot.Children.Add(new NodeSnapshot { Name = "Child" });

        // Assert
        Assert.Equal("TestNode", snapshot.Name);
        Assert.Single(snapshot.Properties);
        Assert.Single(snapshot.Children);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void ExportFromList_LargeList_HandlesCorrectly()
    {
        // Arrange
        var list = Enumerable.Range(1, 1000).ToList();

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("LargeList", list);

        // Assert
        Assert.Equal(1000, snapshot.ListItems.Count);
        Assert.Equal(1, snapshot.ListItems[0]);
        Assert.Equal(1000, snapshot.ListItems[999]);
    }

    [Fact]
    public void ExportFromDictionary_LargeDictionary_HandlesCorrectly()
    {
        // Arrange
        var dict = new Hashtable();
        for (int i = 0; i < 100; i++)
        {
            dict[$"Key{i}"] = $"Value{i}";
        }

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromDictionary("LargeDict", dict);

        // Assert
        Assert.Equal(100, snapshot.DictItems.Count);
    }

    [Fact]
    public void ExportFromList_WithComplexObjects_ReturnsReferences()
    {
        // Arrange
        var obj1 = new { Id = 1, Name = "Object1" };
        var obj2 = new { Id = 2, Name = "Object2" };
        var list = new ArrayList { obj1, obj2 };

        // Act
        var snapshot = MultiValueSnapshotService.ExportFromList("ObjectList", list);

        // Assert
        Assert.Equal(2, snapshot.ListItems.Count);
        Assert.Same(obj1, snapshot.ListItems[0]);
        Assert.Same(obj2, snapshot.ListItems[1]);
    }

    #endregion
}
