using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// BubblingChange 结构体测试
/// </summary>
public class BubblingChangeTests
{
    [Fact]
    public void BubblingChange_DefaultPathSegments_IsEmptyArray()
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Assert
        Assert.NotNull(change.PathSegments);
        Assert.Empty(change.PathSegments);
    }

    [Fact]
    public void BubblingChange_WithPathSegments_ReturnsCorrectPath()
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child", "Grandchild" }
        };

        // Assert
        Assert.Equal(3, change.PathSegments.Count);
        Assert.Equal("Root", change.PathSegments[0]);
        Assert.Equal("Child", change.PathSegments[1]);
        Assert.Equal("Grandchild", change.PathSegments[2]);
    }

    [Fact]
    public void BubblingChange_WithOldAndNewValue_StoresCorrectly()
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Value",
            Kind = NodeChangeKind.PropertyUpdate,
            OldValue = 100,
            NewValue = 200
        };

        // Assert
        Assert.Equal(100, change.OldValue);
        Assert.Equal(200, change.NewValue);
    }

    [Fact]
    public void BubblingChange_WithIndex_StoresCorrectly()
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionAdd,
            Index = 5,
            NewValue = "NewItem"
        };

        // Assert
        Assert.Equal(5, change.Index);
    }

    [Fact]
    public void BubblingChange_WithKey_StoresCorrectly()
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Dictionary",
            Kind = NodeChangeKind.CollectionAdd,
            Key = "MyKey",
            NewValue = "MyValue"
        };

        // Assert
        Assert.Equal("MyKey", change.Key);
    }

    [Fact]
    public void BubblingChange_WithExpression_CreatesNewInstance()
    {
        // Arrange
        var original = new BubblingChange
        {
            PropertyName = "Original",
            Kind = NodeChangeKind.PropertyUpdate,
            OldValue = 1,
            NewValue = 2
        };

        // Act
        var modified = original with { NewValue = 3 };

        // Assert
        Assert.Equal(2, original.NewValue);
        Assert.Equal(3, modified.NewValue);
        Assert.Equal("Original", modified.PropertyName);
        Assert.Equal(1, modified.OldValue);
    }

    [Theory]
    [InlineData(NodeChangeKind.PropertyUpdate)]
    [InlineData(NodeChangeKind.CollectionAdd)]
    [InlineData(NodeChangeKind.CollectionRemove)]
    [InlineData(NodeChangeKind.CollectionReplace)]
    [InlineData(NodeChangeKind.CollectionReset)]
    public void BubblingChange_AllKinds_AreSupported(NodeChangeKind kind)
    {
        // Arrange & Act
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = kind
        };

        // Assert
        Assert.Equal(kind, change.Kind);
    }
}
