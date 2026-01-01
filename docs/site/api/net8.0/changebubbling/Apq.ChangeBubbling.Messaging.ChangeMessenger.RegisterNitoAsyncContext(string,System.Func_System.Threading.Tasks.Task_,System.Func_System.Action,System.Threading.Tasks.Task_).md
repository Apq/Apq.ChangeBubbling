#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.RegisterNitoAsyncContext\(string, Func\<Task\>, Func\<Action,Task\>\) Method

注册基于 Nito\.AsyncEx 的 AsyncContext 线程环境（作为发布执行器）。
观察侧仍可使用线程池或调用方自定义的 Observe 调度。

```csharp
public static void RegisterNitoAsyncContext(string name, System.Func<System.Threading.Tasks.Task> warmup, System.Func<System.Action,System.Threading.Tasks.Task> run);
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterNitoAsyncContext(string,System.Func_System.Threading.Tasks.Task_,System.Func_System.Action,System.Threading.Tasks.Task_).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterNitoAsyncContext(string,System.Func_System.Threading.Tasks.Task_,System.Func_System.Action,System.Threading.Tasks.Task_).warmup'></a>

`warmup` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 'System\.Func\`1')[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 'System\.Func\`1')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterNitoAsyncContext(string,System.Func_System.Threading.Tasks.Task_,System.Func_System.Action,System.Threading.Tasks.Task_).run'></a>

`run` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System\.Action')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')