using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Snapshot;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// TreeSnapshotService 快照服务测试
/// </summary>
[Collection("Sequential")]
public class TreeSnapshotServiceTests
{
    /// <summary>
    /// 测试用的具体节点实现
    /// </summary>
    private class TestNode : ChangeNodeBase
    {
        public TestNode(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// 支持快照序列化的测试节点
    /// </summary>
    private class SerializableTestNode : ChangeNodeBase, ISnapshotSerializable
    {
        public SerializableTestNode(string name)
        {
            Name = name;
        }

        public int Value { get; set; }
        public string? Description { get; set; }

        public Dictionary<string, object?> GetSnapshotProperties()
        {
            return new Dictionary<string, object?>
            {
                ["Value"] = Value,
                ["Description"] = Description
            };
        }

        public void ApplySnapshotProperties(Dictionary<string, object?> properties)
        {
            if (properties.TryGetValue("Value", out var value) && value is int intValue)
            {
                Value = intValue;
            }
            if (properties.TryGetValue("Description", out var desc))
            {
                Description = desc?.ToString();
            }
        }
    }

    [Fact]
    public void Export_SingleNode_ReturnsCorrectSnapshot()
    {
        // Arrange
        var node = new TestNode("Root");

        // Act
        var snapshot = TreeSnapshotService.Export(node);

        // Assert
        Assert.NotNull(snapshot);
        Assert.Equal("Root", snapshot.Name);
        Assert.Empty(snapshot.Children);
        Assert.Empty(snapshot.Properties);
    }

    [Fact]
    public void Export_NodeWithChildren_ReturnsHierarchy()
    {
        // Arrange
        var root = new TestNode("Root");
        var child1 = new TestNode("Child1");
        var child2 = new TestNode("Child2");
        root.AttachChild(child1);
        root.AttachChild(child2);

        // Act
        var snapshot = TreeSnapshotService.Export(root);

        // Assert
        Assert.Equal("Root", snapshot.Name);
        Assert.Equal(2, snapshot.Children.Count);
        Assert.Contains(snapshot.Children, c => c.Name == "Child1");
        Assert.Contains(snapshot.Children, c => c.Name == "Child2");
    }

    [Fact]
    public void Export_DeepHierarchy_ReturnsAllLevels()
    {
        // Arrange
        var root = new TestNode("Root");
        var level1 = new TestNode("Level1");
        var level2 = new TestNode("Level2");
        var level3 = new TestNode("Level3");
        root.AttachChild(level1);
        level1.AttachChild(level2);
        level2.AttachChild(level3);

        // Act
        var snapshot = TreeSnapshotService.Export(root);

        // Assert
        Assert.Equal("Root", snapshot.Name);
        Assert.Single(snapshot.Children);
        Assert.Equal("Level1", snapshot.Children[0].Name);
        Assert.Single(snapshot.Children[0].Children);
        Assert.Equal("Level2", snapshot.Children[0].Children[0].Name);
        Assert.Single(snapshot.Children[0].Children[0].Children);
        Assert.Equal("Level3", snapshot.Children[0].Children[0].Children[0].Name);
    }

    [Fact]
    public void Export_SerializableNode_IncludesProperties()
    {
        // Arrange
        var node = new SerializableTestNode("TestNode")
        {
            Value = 42,
            Description = "Test Description"
        };

        // Act
        var snapshot = TreeSnapshotService.Export(node);

        // Assert
        Assert.Equal("TestNode", snapshot.Name);
        Assert.Equal(2, snapshot.Properties.Count);
        Assert.Equal(42, snapshot.Properties["Value"]);
        Assert.Equal("Test Description", snapshot.Properties["Description"]);
    }

    [Fact]
    public void Export_NullNode_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TreeSnapshotService.Export(null!));
    }

    [Fact]
    public void Import_SimpleSnapshot_CreatesNode()
    {
        // Arrange
        var snapshot = new NodeSnapshot { Name = "ImportedNode" };

        // Act
        var node = TreeSnapshotService.Import(snapshot);

        // Assert
        Assert.NotNull(node);
        Assert.Equal("ImportedNode", node.Name);
        Assert.Empty(node.Children);
    }

    [Fact]
    public void Import_SnapshotWithChildren_CreatesHierarchy()
    {
        // Arrange
        var snapshot = new NodeSnapshot
        {
            Name = "Root",
            Children = new List<NodeSnapshot>
            {
                new NodeSnapshot { Name = "Child1" },
                new NodeSnapshot { Name = "Child2" }
            }
        };

        // Act
        var node = TreeSnapshotService.Import(snapshot);

        // Assert
        Assert.Equal("Root", node.Name);
        Assert.Equal(2, node.Children.Count);
        Assert.Contains(node.Children, c => c.Name == "Child1");
        Assert.Contains(node.Children, c => c.Name == "Child2");
    }

    [Fact]
    public void Import_DeepSnapshot_CreatesDeepHierarchy()
    {
        // Arrange
        var snapshot = new NodeSnapshot
        {
            Name = "Root",
            Children = new List<NodeSnapshot>
            {
                new NodeSnapshot
                {
                    Name = "Level1",
                    Children = new List<NodeSnapshot>
                    {
                        new NodeSnapshot
                        {
                            Name = "Level2",
                            Children = new List<NodeSnapshot>
                            {
                                new NodeSnapshot { Name = "Level3" }
                            }
                        }
                    }
                }
            }
        };

        // Act
        var node = TreeSnapshotService.Import(snapshot);

        // Assert
        Assert.Equal("Root", node.Name);
        Assert.Single(node.Children);
        Assert.Equal("Level1", node.Children[0].Name);
        Assert.Single(node.Children[0].Children);
        Assert.Equal("Level2", node.Children[0].Children[0].Name);
        Assert.Single(node.Children[0].Children[0].Children);
        Assert.Equal("Level3", node.Children[0].Children[0].Children[0].Name);
    }

    [Fact]
    public void Import_NullSnapshot_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TreeSnapshotService.Import(null!));
    }

    [Fact]
    public void ImportInto_AppliesPropertiesToSerializableNode()
    {
        // Arrange
        var target = new SerializableTestNode("Target");
        var snapshot = new NodeSnapshot
        {
            Name = "Target",
            Properties = new Dictionary<string, object?>
            {
                ["Value"] = 100,
                ["Description"] = "Imported Description"
            }
        };

        // Act
        TreeSnapshotService.ImportInto(target, snapshot);

        // Assert
        Assert.Equal(100, target.Value);
        Assert.Equal("Imported Description", target.Description);
    }

    [Fact]
    public void ImportInto_CreatesNewChildrenIfNotExist()
    {
        // Arrange
        var target = new TestNode("Root");
        var snapshot = new NodeSnapshot
        {
            Name = "Root",
            Children = new List<NodeSnapshot>
            {
                new NodeSnapshot { Name = "NewChild1" },
                new NodeSnapshot { Name = "NewChild2" }
            }
        };

        // Act
        TreeSnapshotService.ImportInto(target, snapshot);

        // Assert
        Assert.Equal(2, target.Children.Count);
        Assert.Contains(target.Children, c => c.Name == "NewChild1");
        Assert.Contains(target.Children, c => c.Name == "NewChild2");
    }

    [Fact]
    public void ImportInto_UpdatesExistingChildren()
    {
        // Arrange
        var target = new SerializableTestNode("Root");
        var existingChild = new SerializableTestNode("Child") { Value = 10 };
        target.AttachChild(existingChild);

        var snapshot = new NodeSnapshot
        {
            Name = "Root",
            Children = new List<NodeSnapshot>
            {
                new NodeSnapshot
                {
                    Name = "Child",
                    Properties = new Dictionary<string, object?>
                    {
                        ["Value"] = 99,
                        ["Description"] = "Updated"
                    }
                }
            }
        };

        // Act
        TreeSnapshotService.ImportInto(target, snapshot);

        // Assert
        Assert.Single(target.Children);
        Assert.Equal(99, existingChild.Value);
        Assert.Equal("Updated", existingChild.Description);
    }

    [Fact]
    public void ImportInto_NullTarget_ThrowsArgumentNullException()
    {
        // Arrange
        var snapshot = new NodeSnapshot { Name = "Test" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TreeSnapshotService.ImportInto(null!, snapshot));
    }

    [Fact]
    public void ImportInto_NullSnapshot_ThrowsArgumentNullException()
    {
        // Arrange
        var target = new TestNode("Test");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TreeSnapshotService.ImportInto(target, null!));
    }

    [Fact]
    public void Export_Import_RoundTrip_PreservesStructure()
    {
        // Arrange
        var original = new TestNode("Root");
        var child1 = new TestNode("Child1");
        var grandChild = new TestNode("GrandChild");
        original.AttachChild(child1);
        child1.AttachChild(grandChild);

        // Act
        var snapshot = TreeSnapshotService.Export(original);
        var imported = TreeSnapshotService.Import(snapshot);

        // Assert
        Assert.Equal(original.Name, imported.Name);
        Assert.Equal(original.Children.Count, imported.Children.Count);
        // 查找名为 Child1 的子节点
        var importedChild1 = imported.Children.FirstOrDefault(c => c.Name == "Child1");
        Assert.NotNull(importedChild1);
        Assert.Single(importedChild1.Children);
    }

    [Fact]
    public void Export_Import_RoundTrip_PreservesProperties()
    {
        // Arrange
        var original = new SerializableTestNode("Root")
        {
            Value = 42,
            Description = "Original"
        };

        // Act
        var snapshot = TreeSnapshotService.Export(original);
        var target = new SerializableTestNode("Root");
        TreeSnapshotService.ImportInto(target, snapshot);

        // Assert
        Assert.Equal(original.Value, target.Value);
        Assert.Equal(original.Description, target.Description);
    }
}
