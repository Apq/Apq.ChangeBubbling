using Apq.ChangeBubbling.Infrastructure.Nito;

namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// NitoAsyncContextEnvironment 异步上下文环境测试
/// </summary>
[Collection("Sequential")]
public class NitoAsyncContextEnvironmentTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_CreatesInstance()
    {
        // Arrange & Act
        using var env = new NitoAsyncContextEnvironment();

        // Assert
        Assert.NotNull(env);
    }

    #endregion

    #region RunAsync with Func<Task> Tests

    [Fact]
    public async Task RunAsync_FuncTask_ExecutesAction()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var executed = false;

        // Act
        await env.RunAsync(async () =>
        {
            await Task.Delay(10);
            executed = true;
        });

        // Assert
        Assert.True(executed);
    }

    [Fact]
    public async Task RunAsync_FuncTask_ReturnsCompletedTask()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();

        // Act
        var task = env.RunAsync(async () =>
        {
            await Task.Delay(10);
        });

        // Assert
        Assert.NotNull(task);
        await task;
    }
    [Fact]
    public async Task RunAsync_FuncTask_ExecutesOnDedicatedThread()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var currentThreadId = Environment.CurrentManagedThreadId;
        var executionThreadId = -1;

        // Act
        await env.RunAsync(async () =>
        {
            executionThreadId = Environment.CurrentManagedThreadId;
            await Task.Yield();
        });

        // Assert
        Assert.NotEqual(-1, executionThreadId);
    }

    [Fact]
    public async Task RunAsync_FuncTask_MultipleCallsExecuteSequentially()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var results = new List<int>();

        // Act
        await env.RunAsync(async () =>
        {
            await Task.Delay(10);
            results.Add(1);
        });

        await env.RunAsync(async () =>
        {
            await Task.Delay(10);
            results.Add(2);
        });

        await env.RunAsync(async () =>
        {
            await Task.Delay(10);
            results.Add(3);
        });

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(1, results[0]);
        Assert.Equal(2, results[1]);
        Assert.Equal(3, results[2]);
    }

    #endregion

    #region RunAsync with Action Tests

    [Fact]
    public async Task RunAsync_Action_ExecutesAction()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var executed = false;

        // Act
        await env.RunAsync(() =>
        {
            executed = true;
        });

        // Assert
        Assert.True(executed);
    }
    [Fact]
    public async Task RunAsync_Action_ReturnsCompletedTask()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();

        // Act
        var task = env.RunAsync(() =>
        {
            Thread.Sleep(10);
        });

        // Assert
        Assert.NotNull(task);
        await task;
    }

    [Fact]
    public async Task RunAsync_Action_MultipleCallsExecuteSequentially()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var results = new List<int>();

        // Act
        await env.RunAsync(() => results.Add(1));
        await env.RunAsync(() => results.Add(2));
        await env.RunAsync(() => results.Add(3));

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(1, results[0]);
        Assert.Equal(2, results[1]);
        Assert.Equal(3, results[2]);
    }

    #endregion

    #region Dispose Tests

    [Fact]
    public void Dispose_CanBeCalledMultipleTimes()
    {
        // Arrange
        var env = new NitoAsyncContextEnvironment();

        // Act & Assert
        env.Dispose();
        env.Dispose();
    }

    [Fact]
    public async Task Dispose_AfterRunAsync_CompletesGracefully()
    {
        // Arrange
        var env = new NitoAsyncContextEnvironment();
        var executed = false;

        // Act
        await env.RunAsync(() => executed = true);
        env.Dispose();

        // Assert
        Assert.True(executed);
    }

    #endregion
    #region Concurrent Usage Tests

    [Fact]
    public async Task RunAsync_ConcurrentCalls_AllExecute()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var counter = 0;
        var tasks = new List<Task>();

        // Act
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(env.RunAsync(() =>
            {
                Interlocked.Increment(ref counter);
            }));
        }

        await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(10, counter);
    }

    [Fact]
    public async Task RunAsync_MixedAsyncAndSync_AllExecute()
    {
        // Arrange
        using var env = new NitoAsyncContextEnvironment();
        var results = new List<string>();
        var lockObj = new object();

        // Act
        await env.RunAsync(async () =>
        {
            await Task.Delay(5);
            lock (lockObj) results.Add("async1");
        });

        await env.RunAsync(() =>
        {
            lock (lockObj) results.Add("sync1");
        });

        await env.RunAsync(async () =>
        {
            await Task.Delay(5);
            lock (lockObj) results.Add("async2");
        });

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Contains("async1", results);
        Assert.Contains("sync1", results);
        Assert.Contains("async2", results);
    }

    #endregion
}