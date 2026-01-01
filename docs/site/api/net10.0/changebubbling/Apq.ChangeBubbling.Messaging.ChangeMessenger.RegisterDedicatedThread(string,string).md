#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.RegisterDedicatedThread\(string, string\) Method

注册专用线程事件循环环境（串行执行）。返回可释放的调度器实例。

```csharp
public static System.IDisposable RegisterDedicatedThread(string name, string? threadName=null);
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterDedicatedThread(string,string).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterDedicatedThread(string,string).threadName'></a>

`threadName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable 'System\.IDisposable')