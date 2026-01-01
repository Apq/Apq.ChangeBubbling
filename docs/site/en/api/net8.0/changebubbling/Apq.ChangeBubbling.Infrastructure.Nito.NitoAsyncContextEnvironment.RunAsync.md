#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Nito](Apq.ChangeBubbling.Infrastructure.Nito.md 'Apq\.ChangeBubbling\.Infrastructure\.Nito').[NitoAsyncContextEnvironment](Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.md 'Apq\.ChangeBubbling\.Infrastructure\.Nito\.NitoAsyncContextEnvironment')

## NitoAsyncContextEnvironment\.RunAsync Method

| Overloads | |
| :--- | :--- |
| [RunAsync\(Action\)](Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync.md#Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Action) 'Apq\.ChangeBubbling\.Infrastructure\.Nito\.NitoAsyncContextEnvironment\.RunAsync\(System\.Action\)') | 在专用 AsyncContext 线程上执行同步动作。 |
| [RunAsync\(Func&lt;Task&gt;\)](Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync.md#Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Func_System.Threading.Tasks.Task_) 'Apq\.ChangeBubbling\.Infrastructure\.Nito\.NitoAsyncContextEnvironment\.RunAsync\(System\.Func\<System\.Threading\.Tasks\.Task\>\)') | 在专用 AsyncContext 线程上执行任务。 |

<a name='Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Action)'></a>

## NitoAsyncContextEnvironment\.RunAsync\(Action\) Method

在专用 AsyncContext 线程上执行同步动作。

```csharp
public System.Threading.Tasks.Task RunAsync(System.Action action);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Action).action'></a>

`action` [System\.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System\.Action')

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')

<a name='Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Func_System.Threading.Tasks.Task_)'></a>

## NitoAsyncContextEnvironment\.RunAsync\(Func\<Task\>\) Method

在专用 AsyncContext 线程上执行任务。

```csharp
public System.Threading.Tasks.Task RunAsync(System.Func<System.Threading.Tasks.Task> action);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Func_System.Threading.Tasks.Task_).action'></a>

`action` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 'System\.Func\`1')[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 'System\.Func\`1')

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')