using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Infrastructure.EventFiltering;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// 事件过滤器测试
/// </summary>
[Collection("Sequential")]
public class EventFilterTests
{
    #region PropertyBasedEventFilter Tests

    [Fact]
    public void PropertyBasedEventFilter_NoFilters_AllowsAll()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter();
        var change = new BubblingChange
        {
            PropertyName = "AnyProperty",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act & Assert
        Assert.True(filter.ShouldProcess(change));
        Assert.True(filter.ShouldBubble(change));
    }

    [Fact]
    public void PropertyBasedEventFilter_AllowedProperties_OnlyAllowsMatching()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name", "Value" }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Value",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_ExcludedProperties_BlocksMatching()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            excludedProperties: new[] { "Internal", "Private" }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Public",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Internal",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Private",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_AllowedKinds_OnlyAllowsMatching()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedKinds: new[] { NodeChangeKind.CollectionAdd, NodeChangeKind.CollectionRemove }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionAdd
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionRemove
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_ExcludedKinds_BlocksMatching()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            excludedKinds: new[] { NodeChangeKind.CollectionReset }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionAdd
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionReset
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_ExactMatch_UsesExactComparison()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" },
            useExactMatch: true
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "FullName",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_FuzzyMatch_UsesContains()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" },
            useExactMatch: false
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "FullName",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "UserName",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_CombinedFilters_AppliesBoth()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Items" },
            allowedKinds: new[] { NodeChangeKind.CollectionAdd }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionAdd
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Items",
            Kind = NodeChangeKind.CollectionRemove
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.CollectionAdd
        }));
    }

    [Fact]
    public void PropertyBasedEventFilter_Name_ReturnsCorrectName()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter();

        // Assert
        Assert.Equal("PropertyBasedEventFilter", filter.Name);
    }

    [Fact]
    public void PropertyBasedEventFilter_Description_ReturnsDescription()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter();

        // Assert
        Assert.NotNull(filter.Description);
    }

    #endregion

    #region PathBasedEventFilter Tests

    [Fact]
    public void PathBasedEventFilter_NoFilters_AllowsAll()
    {
        // Arrange
        var filter = new PathBasedEventFilter();
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child" }
        };

        // Act & Assert
        Assert.True(filter.ShouldProcess(change));
    }

    [Fact]
    public void PathBasedEventFilter_AllowedPaths_OnlyAllowsMatching()
    {
        // Arrange
        var filter = new PathBasedEventFilter(
            allowedPaths: new[] { "Users", "Settings" }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Users", "Admin" }
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Value",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Settings", "Theme" }
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Data",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Cache", "Temp" }
        }));
    }

    [Fact]
    public void PathBasedEventFilter_ExcludedPaths_BlocksMatching()
    {
        // Arrange
        var filter = new PathBasedEventFilter(
            excludedPaths: new[] { "Internal", "Debug" }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Public", "Data" }
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Value",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Internal", "State" }
        }));
    }

    [Fact]
    public void PathBasedEventFilter_MaxDepth_BlocksDeepPaths()
    {
        // Arrange
        var filter = new PathBasedEventFilter(maxDepth: 2);

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Level1", "Level2" }
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Level1", "Level2", "Level3" }
        }));
    }

    [Fact]
    public void PathBasedEventFilter_EmptyPath_HandledCorrectly()
    {
        // Arrange
        var filter = new PathBasedEventFilter();
        var change = new BubblingChange
        {
            PropertyName = "Root",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = Array.Empty<string>()
        };

        // Act & Assert
        Assert.True(filter.ShouldProcess(change));
    }

    [Fact]
    public void PathBasedEventFilter_DottedPattern_MatchesFullPath()
    {
        // Arrange
        var filter = new PathBasedEventFilter(
            allowedPaths: new[] { "Root.Child" }
        );

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child", "Grandchild" }
        }));
    }

    [Fact]
    public void PathBasedEventFilter_Name_ReturnsCorrectName()
    {
        // Arrange
        var filter = new PathBasedEventFilter();

        // Assert
        Assert.Equal("PathBasedEventFilter", filter.Name);
    }

    #endregion

    #region FrequencyBasedEventFilter Tests

    [Fact]
    public void FrequencyBasedEventFilter_FirstEvent_IsProcessed()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act & Assert
        Assert.True(filter.ShouldProcess(change));
    }

    [Fact]
    public void FrequencyBasedEventFilter_RapidEvents_AreThrottled()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        var first = filter.ShouldProcess(change);
        var second = filter.ShouldProcess(change);
        var third = filter.ShouldProcess(change);

        // Assert
        Assert.True(first);
        Assert.False(second);
        Assert.False(third);
    }

    [Fact]
    public async Task FrequencyBasedEventFilter_AfterInterval_AllowsAgain()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(50));
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        var first = filter.ShouldProcess(change);
        await Task.Delay(100);
        var second = filter.ShouldProcess(change);

        // Assert
        Assert.True(first);
        Assert.True(second);
    }

    [Fact]
    public void FrequencyBasedEventFilter_DifferentProperties_TrackedSeparately()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));

        // Act & Assert
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.True(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Prop2",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(filter.ShouldProcess(new BubblingChange
        {
            PropertyName = "Prop1",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void FrequencyBasedEventFilter_DifferentContexts_TrackedSeparately()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act & Assert
        Assert.True(filter.ShouldProcess(change, "Context1"));
        Assert.True(filter.ShouldProcess(change, "Context2"));
        Assert.False(filter.ShouldProcess(change, "Context1"));
    }

    [Fact]
    public void FrequencyBasedEventFilter_Reset_ClearsState()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act
        filter.ShouldProcess(change);
        filter.Reset();
        var afterReset = filter.ShouldProcess(change);

        // Assert
        Assert.True(afterReset);
    }

    [Fact]
    public void FrequencyBasedEventFilter_Name_ReturnsCorrectName()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));

        // Assert
        Assert.Equal("FrequencyBasedEventFilter", filter.Name);
    }

    [Fact]
    public void FrequencyBasedEventFilter_Description_IncludesInterval()
    {
        // Arrange
        var filter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));

        // Assert
        Assert.NotNull(filter.Description);
        Assert.Contains("100", filter.Description);
    }

    #endregion

    #region CompositeEventFilter Tests

    [Fact]
    public void CompositeEventFilter_AllMode_RequiresAllFiltersToPass()
    {
        // Arrange
        var propertyFilter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" }
        );
        var kindFilter = new PropertyBasedEventFilter(
            allowedKinds: new[] { NodeChangeKind.PropertyUpdate }
        );
        var composite = new CompositeEventFilter(
            new IChangeEventFilter[] { propertyFilter, kindFilter },
            CompositeEventFilter.FilterMode.All
        );

        // Act & Assert
        Assert.True(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.CollectionAdd
        }));
        Assert.False(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void CompositeEventFilter_AnyMode_RequiresOneFilterToPass()
    {
        // Arrange
        var propertyFilter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" },
            useExactMatch: true
        );
        var kindFilter = new PropertyBasedEventFilter(
            allowedKinds: new[] { NodeChangeKind.CollectionAdd }
        );
        var composite = new CompositeEventFilter(
            new IChangeEventFilter[] { propertyFilter, kindFilter },
            CompositeEventFilter.FilterMode.Any
        );

        // Act & Assert
        Assert.True(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.True(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.CollectionAdd
        }));
        Assert.False(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void CompositeEventFilter_EmptyFilters_AllModeReturnsTrue()
    {
        // Arrange
        var composite = new CompositeEventFilter(
            Array.Empty<IChangeEventFilter>(),
            CompositeEventFilter.FilterMode.All
        );
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act & Assert
        Assert.True(composite.ShouldProcess(change));
    }

    [Fact]
    public void CompositeEventFilter_EmptyFilters_AnyModeReturnsFalse()
    {
        // Arrange
        var composite = new CompositeEventFilter(
            Array.Empty<IChangeEventFilter>(),
            CompositeEventFilter.FilterMode.Any
        );
        var change = new BubblingChange
        {
            PropertyName = "Test",
            Kind = NodeChangeKind.PropertyUpdate
        };

        // Act & Assert
        Assert.False(composite.ShouldProcess(change));
    }

    [Fact]
    public void CompositeEventFilter_ShouldBubble_FollowsSameLogic()
    {
        // Arrange
        var filter = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" }
        );
        var composite = new CompositeEventFilter(
            new IChangeEventFilter[] { filter },
            CompositeEventFilter.FilterMode.All
        );

        // Act & Assert
        Assert.True(composite.ShouldBubble(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate
        }));
        Assert.False(composite.ShouldBubble(new BubblingChange
        {
            PropertyName = "Other",
            Kind = NodeChangeKind.PropertyUpdate
        }));
    }

    [Fact]
    public void CompositeEventFilter_NullFilters_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CompositeEventFilter(null!));
    }

    [Fact]
    public void CompositeEventFilter_Name_ReturnsCorrectName()
    {
        // Arrange
        var composite = new CompositeEventFilter(Array.Empty<IChangeEventFilter>());

        // Assert
        Assert.Equal("CompositeEventFilter", composite.Name);
    }

    [Fact]
    public void CompositeEventFilter_Description_IncludesMode()
    {
        // Arrange
        var composite = new CompositeEventFilter(
            Array.Empty<IChangeEventFilter>(),
            CompositeEventFilter.FilterMode.Any
        );

        // Assert
        Assert.NotNull(composite.Description);
        Assert.Contains("Any", composite.Description);
    }

    [Fact]
    public void CompositeEventFilter_NestedComposite_WorksCorrectly()
    {
        // Arrange
        var innerFilter1 = new PropertyBasedEventFilter(
            allowedProperties: new[] { "Name" }
        );
        var innerFilter2 = new PropertyBasedEventFilter(
            allowedKinds: new[] { NodeChangeKind.PropertyUpdate }
        );
        var innerComposite = new CompositeEventFilter(
            new IChangeEventFilter[] { innerFilter1, innerFilter2 },
            CompositeEventFilter.FilterMode.All
        );
        var outerFilter = new PathBasedEventFilter(maxDepth: 3);
        var outerComposite = new CompositeEventFilter(
            new IChangeEventFilter[] { innerComposite, outerFilter },
            CompositeEventFilter.FilterMode.All
        );

        // Act & Assert
        Assert.True(outerComposite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child" }
        }));
        Assert.False(outerComposite.ShouldProcess(new BubblingChange
        {
            PropertyName = "Name",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "L1", "L2", "L3", "L4" }
        }));
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void AllFilters_ImplementIChangeEventFilter()
    {
        // Arrange & Act
        IChangeEventFilter propertyFilter = new PropertyBasedEventFilter();
        IChangeEventFilter pathFilter = new PathBasedEventFilter();
        IChangeEventFilter frequencyFilter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(100));
        IChangeEventFilter compositeFilter = new CompositeEventFilter(Array.Empty<IChangeEventFilter>());

        // Assert
        Assert.NotNull(propertyFilter.Name);
        Assert.NotNull(pathFilter.Name);
        Assert.NotNull(frequencyFilter.Name);
        Assert.NotNull(compositeFilter.Name);
    }

    [Fact]
    public void ComplexFilterChain_WorksCorrectly()
    {
        // Arrange
        var propertyFilter = new PropertyBasedEventFilter(
            excludedProperties: new[] { "Internal" }
        );
        var pathFilter = new PathBasedEventFilter(
            excludedPaths: new[] { "Debug" },
            maxDepth: 5
        );
        var frequencyFilter = new FrequencyBasedEventFilter(TimeSpan.FromMilliseconds(10));
        var composite = new CompositeEventFilter(
            new IChangeEventFilter[] { propertyFilter, pathFilter, frequencyFilter },
            CompositeEventFilter.FilterMode.All
        );

        // Act & Assert
        Assert.True(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "PublicProp",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child" }
        }));
        Assert.False(composite.ShouldProcess(new BubblingChange
        {
            PropertyName = "InternalProp",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child" }
        }));
    }

    #endregion
}
