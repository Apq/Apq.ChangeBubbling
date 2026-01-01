#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging')

## ChangeMessenger Class

冒泡事件消息中心：弱引用消息 \+ Rx 事件流 \+ 可插拔调度环境。

```csharp
public static class ChangeMessenger
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; ChangeMessenger

| Properties | |
| :--- | :--- |
| [EnableMetrics](Apq.ChangeBubbling.Messaging.ChangeMessenger.EnableMetrics.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.EnableMetrics') | 是否启用性能指标收集。高性能场景下可禁用以减少开销。 |
| [EnableRxStream](Apq.ChangeBubbling.Messaging.ChangeMessenger.EnableRxStream.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.EnableRxStream') | 是否启用 Rx Subject 发布。如果只使用 WeakReferenceMessenger 可禁用。 |
| [EnableWeakMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.EnableWeakMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.EnableWeakMessenger') | 是否启用 WeakReferenceMessenger 发布。如果只使用 Rx 流可禁用。 |
| [UseSynchronousPublish](Apq.ChangeBubbling.Messaging.ChangeMessenger.UseSynchronousPublish.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.UseSynchronousPublish') | 是否使用同步发布模式（直接调用 OnNext，不经过调度器）。 启用后可减少 Lambda 闭包分配和调度延迟，但会在调用线程上执行。 适用于不需要线程切换的高性能场景。 |

| Methods | |
| :--- | :--- |
| [AsBufferedObservable\(TimeSpan, int, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.AsBufferedObservable(System.TimeSpan,int,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.AsBufferedObservable\(System\.TimeSpan, int, string\)') | 获取指定环境的缓冲批量事件流。满足时间窗口或数量窗口任一条件即触发。 |
| [AsObservable\(string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.AsObservable(string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.AsObservable\(string\)') | 获取指定环境的原始事件流。 |
| [AsThrottledObservable\(Nullable&lt;TimeSpan&gt;, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.AsThrottledObservable(System.Nullable_System.TimeSpan_,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.AsThrottledObservable\(System\.Nullable\<System\.TimeSpan\>, string\)') | 获取指定环境的节流事件流（默认 50ms）。 |
| [AsWindowedObservable\(TimeSpan, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.AsWindowedObservable(System.TimeSpan,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.AsWindowedObservable\(System\.TimeSpan, string\)') | 获取指定环境的时间窗口切片事件流（Window）。 |
| [GetEventTypeStatistics\(\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.GetEventTypeStatistics().md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.GetEventTypeStatistics\(\)') | 获取事件类型统计。 |
| [GetPerformanceMetrics\(\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.GetPerformanceMetrics().md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.GetPerformanceMetrics\(\)') | 获取性能指标。 |
| [Publish\(BubblingChange, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.Publish(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.Publish\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 发布冒泡事件到指定环境（未注册则落到默认）。 |
| [PublishToDefaultEnv\(BubblingChange\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.PublishToDefaultEnv(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.PublishToDefaultEnv\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 默认环境的快速发布路径。 |
| [PublishToNamedEnv\(BubblingChange, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.PublishToNamedEnv(Apq.ChangeBubbling.Abstractions.BubblingChange,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.PublishToNamedEnv\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange, string\)') | 命名环境的发布路径。 |
| [RegisterDedicatedThread\(string, string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterDedicatedThread(string,string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RegisterDedicatedThread\(string, string\)') | 注册专用线程事件循环环境（串行执行）。返回可释放的调度器实例。 |
| [RegisterDispatcher\(string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterDispatcher(string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RegisterDispatcher\(string\)') | 注册基于当前 SynchronizationContext 的环境。需在目标线程调用。 |
| [RegisterFilter\(string, IChangeEventFilter\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterFilter(string,Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RegisterFilter\(string, Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter\)') | 注册事件过滤器。 |
| [RegisterNitoAsyncContext\(string, Func&lt;Task&gt;, Func&lt;Action,Task&gt;\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterNitoAsyncContext(string,System.Func_System.Threading.Tasks.Task_,System.Func_System.Action,System.Threading.Tasks.Task_).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RegisterNitoAsyncContext\(string, System\.Func\<System\.Threading\.Tasks\.Task\>, System\.Func\<System\.Action,System\.Threading\.Tasks\.Task\>\)') | 注册基于 Nito\.AsyncEx 的 AsyncContext 线程环境（作为发布执行器）。 观察侧仍可使用线程池或调用方自定义的 Observe 调度。 |
| [RegisterThreadPool\(string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterThreadPool(string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RegisterThreadPool\(string\)') | 注册线程池环境。 |
| [RemoveFilter\(string\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.RemoveFilter(string).md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.RemoveFilter\(string\)') | 移除事件过滤器。 |
| [Reset\(\)](Apq.ChangeBubbling.Messaging.ChangeMessenger.Reset().md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger\.Reset\(\)') | 重置消息中心状态（用于测试）。 清除所有环境、流、过滤器，并重新注册默认线程池环境。 |
