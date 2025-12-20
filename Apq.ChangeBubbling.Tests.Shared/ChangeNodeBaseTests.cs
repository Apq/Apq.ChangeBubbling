using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Core;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// ChangeNodeBase 基类测试
/// </summary>
public class ChangeNodeBaseTests
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

        public void TriggerChange(BubblingChange change)
        {
            RaiseNodeChanged(change);
        }

        public void TriggerChangeBatched(BubblingChange change)
        {
            RaiseNodeChangedBatched(change);
        }

        public void TriggerChangeCoalesced(BubblingChange change)
        {
            RaiseNodeChangedCoalesced(change);
        }
    }

    [Fact]
    public void AttachChild_AddsChildToCollection()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");

        // Act
        parent.AttachChild(child);

        // Assert
        Assert.Single(parent.Children);
        Assert.Same(child, parent.Children[0]);
        Assert.Same(parent, child.Parent);
    }

    [Fact]
    public void DetachChild_RemovesChildFromCollection()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");
        parent.AttachChild(child);

        // Act
        parent.DetachChild(child);

        // Assert
        Assert.Empty(parent.Children);
        Assert.Null(child.Parent);
    }

    [Fact]
    public void AttachChild_Self_ThrowsException()
    {
        // Arrange
        var node = new TestNode("Node");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => node.AttachChild(node));
    }

    [Fact]
    public void AttachChild_CircularReference_ThrowsException()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");
        parent.AttachChild(child);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => child.AttachChild(parent));
    }

    [Fact]
    public void NodeChanged_BubblesUpToParent()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");
        parent.AttachChild(child);

        BubblingChange? receivedChange = null;
        parent.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        child.TriggerChange(new BubblingChange
        {
            PropertyName = "TestProperty",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Child" }
        });

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal("TestProperty", receivedChange.Value.PropertyName);
        Assert.Equal(NodeChangeKind.PropertyUpdate, receivedChange.Value.Kind);
    }

    [Fact]
    public void BeginBatch_EndBatch_CollectsAndRaisesChanges()
    {
        // Arrange
        var node = new TestNode("Node");
        var receivedChanges = new List<BubblingChange>();
        node.NodeChanged += (sender, change) => receivedChanges.Add(change);

        // Act
        node.BeginBatch();
        Assert.True(node.IsInBatch);

        node.TriggerChangeBatched(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate
        });
        node.TriggerChangeBatched(new BubblingChange
        {
            PropertyName = "Prop2",
            Kind = NodeChangeKind.PropertyUpdate
        });

        // 批量模式下事件被收集
        Assert.Empty(receivedChanges);

        node.EndBatch();
        Assert.False(node.IsInBatch);

        // 结束批量后事件被触发
        Assert.Equal(2, receivedChanges.Count);
    }

    [Fact]
    public void BeginCoalesce_EndCoalesce_MergesSamePropertyChanges()
    {
        // Arrange
        var node = new TestNode("Node");
        var receivedChanges = new List<BubblingChange>();
        node.NodeChanged += (sender, change) => receivedChanges.Add(change);

        // Act
        node.BeginCoalesce();
        Assert.True(node.IsCoalescing);

        // 同一属性多次变更
        node.TriggerChangeCoalesced(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate,
            OldValue = 1,
            NewValue = 2
        });
        node.TriggerChangeCoalesced(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate,
            OldValue = 2,
            NewValue = 3
        });
        node.TriggerChangeCoalesced(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate,
            OldValue = 3,
            NewValue = 4
        });

        // 合并模式下事件被收集
        Assert.Empty(receivedChanges);

        node.EndCoalesce();
        Assert.False(node.IsCoalescing);

        // 结束合并后只触发一次事件，OldValue 是第一次的，NewValue 是最后一次的
        Assert.Single(receivedChanges);
        Assert.Equal(1, receivedChanges[0].OldValue);
        Assert.Equal(4, receivedChanges[0].NewValue);
    }

    [Fact]
    public void MultipleChildren_AllBubbleToParent()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child1 = new TestNode("Child1");
        var child2 = new TestNode("Child2");
        parent.AttachChild(child1);
        parent.AttachChild(child2);

        var receivedChanges = new List<BubblingChange>();
        parent.NodeChanged += (sender, change) => receivedChanges.Add(change);

        // Act
        child1.TriggerChange(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Child1" }
        });
        child2.TriggerChange(new BubblingChange
        {
            PropertyName = "Prop2",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Child2" }
        });

        // Assert
        Assert.Equal(2, receivedChanges.Count);
    }

    [Fact]
    public void DeepHierarchy_BubblesCorrectly()
    {
        // Arrange
        var root = new TestNode("Root");
        var level1 = new TestNode("Level1");
        var level2 = new TestNode("Level2");
        var level3 = new TestNode("Level3");

        root.AttachChild(level1);
        level1.AttachChild(level2);
        level2.AttachChild(level3);

        BubblingChange? receivedChange = null;
        root.NodeChanged += (sender, change) => receivedChange = change;

        // Act
        level3.TriggerChange(new BubblingChange
        {
            PropertyName = "DeepProperty",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Level3" }
        });

        // Assert
        Assert.NotNull(receivedChange);
        Assert.Equal("DeepProperty", receivedChange.Value.PropertyName);
    }

    [Fact]
    public void NestedBatch_OnlyOutermostEndBatchTriggersEvents()
    {
        // Arrange
        var node = new TestNode("Node");
        var receivedChanges = new List<BubblingChange>();
        node.NodeChanged += (sender, change) => receivedChanges.Add(change);

        // Act
        node.BeginBatch();
        node.BeginBatch(); // 嵌套

        node.TriggerChangeBatched(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate
        });

        node.EndBatch(); // 内层结束
        Assert.True(node.IsInBatch); // 仍在批量模式
        Assert.Empty(receivedChanges);

        node.EndBatch(); // 外层结束
        Assert.False(node.IsInBatch);
        Assert.Single(receivedChanges);
    }

    [Fact]
    public void AttachChild_Duplicate_DoesNotAddTwice()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");

        // Act
        parent.AttachChild(child);
        parent.AttachChild(child); // 重复添加

        // Assert
        Assert.Single(parent.Children);
    }

    [Fact]
    public void DetachChild_NotAttached_DoesNotThrow()
    {
        // Arrange
        var parent = new TestNode("Parent");
        var child = new TestNode("Child");

        // Act & Assert - 不应抛出异常
        parent.DetachChild(child);
    }
}
