using Apq.ChangeBubbling.Snapshot;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// SnapshotSerializer 序列化测试
/// </summary>
[Collection("Sequential")]
public class SnapshotSerializerTests
{
    #region NodeSnapshot ToJson/FromJson Tests

    [Fact]
    public void ToJson_NodeSnapshot_ReturnsValidJson()
    {
        // Arrange
        var snapshot = new NodeSnapshot
        {
            Name = "TestNode",
            Properties = new Dictionary<string, object?>
            {
                ["Key1"] = "Value1",
                ["Key2"] = 42
            }
        };

        // Act
        var json = SnapshotSerializer.ToJson(snapshot);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("TestNode", json);
        Assert.Contains("Key1", json);
        Assert.Contains("Value1", json);
    }

    [Fact]
    public void FromJson_ValidJson_ReturnsNodeSnapshot()
    {
        // Arrange
        var original = new NodeSnapshot
        {
            Name = "TestNode",
            Properties = new Dictionary<string, object?>
            {
                ["StringProp"] = "Hello"
            }
        };
        var json = SnapshotSerializer.ToJson(original);

        // Act
        var result = SnapshotSerializer.FromJson(json);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestNode", result.Name);
    }

    [Fact]
    public void FromJson_EmptyString_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJson("");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result.Name);
    }

    [Fact]
    public void FromJson_NullString_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJson(null!);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void FromJson_WhitespaceString_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJson("   ");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void FromJson_InvalidJson_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJson("{ invalid json }");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result.Name);
    }

    [Fact]
    public void ToJson_FromJson_RoundTrip_PreservesData()
    {
        // Arrange
        var original = new NodeSnapshot
        {
            Name = "RoundTripNode",
            Children = new List<NodeSnapshot>
            {
                new NodeSnapshot { Name = "Child1" },
                new NodeSnapshot { Name = "Child2" }
            }
        };

        // Act
        var json = SnapshotSerializer.ToJson(original);
        var result = SnapshotSerializer.FromJson(json);

        // Assert
        Assert.Equal(original.Name, result.Name);
        Assert.Equal(original.Children.Count, result.Children.Count);
    }

    #endregion

    #region MultiValueSnapshot ToJson/FromJsonMulti Tests

    [Fact]
    public void ToJson_MultiValueSnapshot_ReturnsValidJson()
    {
        // Arrange
        var snapshot = new MultiValueSnapshot
        {
            Name = "TestList",
            Kind = "List"
        };
        snapshot.ListItems.Add("Item1");
        snapshot.ListItems.Add("Item2");

        // Act
        var json = SnapshotSerializer.ToJson(snapshot);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("TestList", json);
        Assert.Contains("List", json);
    }

    [Fact]
    public void FromJsonMulti_ValidJson_ReturnsMultiValueSnapshot()
    {
        // Arrange
        var original = new MultiValueSnapshot
        {
            Name = "TestDict",
            Kind = "Dictionary"
        };
        original.DictItems["Key1"] = "Value1";
        var json = SnapshotSerializer.ToJson(original);

        // Act
        var result = SnapshotSerializer.FromJsonMulti(json);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestDict", result.Name);
        Assert.Equal("Dictionary", result.Kind);
    }

    [Fact]
    public void FromJsonMulti_EmptyString_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJsonMulti("");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result.Name);
    }

    [Fact]
    public void FromJsonMulti_NullString_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJsonMulti(null!);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void FromJsonMulti_InvalidJson_ReturnsEmptySnapshot()
    {
        // Act
        var result = SnapshotSerializer.FromJsonMulti("not valid json");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result.Name);
    }

    [Fact]
    public void ToJson_FromJsonMulti_RoundTrip_PreservesListData()
    {
        // Arrange
        var original = new MultiValueSnapshot
        {
            Name = "RoundTripList",
            Kind = "List"
        };
        original.ListItems.Add(1);
        original.ListItems.Add(2);
        original.ListItems.Add(3);

        // Act
        var json = SnapshotSerializer.ToJson(original);
        var result = SnapshotSerializer.FromJsonMulti(json);

        // Assert
        Assert.Equal(original.Name, result.Name);
        Assert.Equal(original.Kind, result.Kind);
        Assert.Equal(original.ListItems.Count, result.ListItems.Count);
    }

    #endregion

    #region ToArray Tests

    [Fact]
    public void ToArray_NullInput_ReturnsEmptyArray()
    {
        // Act
        var result = SnapshotSerializer.ToArray<int>(null);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToArray_ArrayInput_ReturnsSameArray()
    {
        // Arrange
        var input = new[] { 1, 2, 3 };

        // Act
        var result = SnapshotSerializer.ToArray(input);

        // Assert
        Assert.Same(input, result);
    }

    [Fact]
    public void ToArray_ListInput_ReturnsArray()
    {
        // Arrange
        var input = new List<string> { "A", "B", "C" };

        // Act
        var result = SnapshotSerializer.ToArray(input);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Equal("A", result[0]);
        Assert.Equal("B", result[1]);
        Assert.Equal("C", result[2]);
    }

    [Fact]
    public void ToArray_EnumerableInput_ReturnsArray()
    {
        // Arrange
        IEnumerable<int> input = Enumerable.Range(1, 5);

        // Act
        var result = SnapshotSerializer.ToArray(input);

        // Assert
        Assert.Equal(5, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(5, result[4]);
    }

    [Fact]
    public void ToArray_EmptyList_ReturnsEmptyArray()
    {
        // Arrange
        var input = new List<int>();

        // Act
        var result = SnapshotSerializer.ToArray(input);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    #endregion
}
