#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Nito](Apq.ChangeBubbling.Infrastructure.Nito.md 'Apq\.ChangeBubbling\.Infrastructure\.Nito')

## NitoAsyncContextEnvironment Class

基于 Nito\.AsyncEx 的专用 AsyncContext 线程环境。

```csharp
public sealed class NitoAsyncContextEnvironment : System.IDisposable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; NitoAsyncContextEnvironment

Implements [System\.IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable 'System\.IDisposable')

| Methods | |
| :--- | :--- |
| [RunAsync\(Action\)](Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync.md#Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Action) 'Apq\.ChangeBubbling\.Infrastructure\.Nito\.NitoAsyncContextEnvironment\.RunAsync\(System\.Action\)') | 在专用 AsyncContext 线程上执行同步动作。 |
| [RunAsync\(Func&lt;Task&gt;\)](Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync.md#Apq.ChangeBubbling.Infrastructure.Nito.NitoAsyncContextEnvironment.RunAsync(System.Func_System.Threading.Tasks.Task_) 'Apq\.ChangeBubbling\.Infrastructure\.Nito\.NitoAsyncContextEnvironment\.RunAsync\(System\.Func\<System\.Threading\.Tasks\.Task\>\)') | 在专用 AsyncContext 线程上执行任务。 |
