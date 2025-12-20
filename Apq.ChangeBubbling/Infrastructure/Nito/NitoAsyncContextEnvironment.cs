using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Apq.ChangeBubbling.Infrastructure.Nito;

/// <summary>
/// 基于 Nito.AsyncEx 的专用 AsyncContext 线程环境。
/// </summary>
public sealed class NitoAsyncContextEnvironment : IDisposable
{
    private readonly AsyncContextThread _thread;

    public NitoAsyncContextEnvironment()
    {
        _thread = new AsyncContextThread();
    }

    /// <summary>
    /// 在专用 AsyncContext 线程上执行任务。
    /// </summary>
    public Task RunAsync(Func<Task> action) => _thread.Factory.Run(action);

    /// <summary>
    /// 在专用 AsyncContext 线程上执行同步动作。
    /// </summary>
    public Task RunAsync(Action action) => _thread.Factory.Run(() => { action(); return Task.CompletedTask; });

    public void Dispose() => _thread.Dispose();
}


