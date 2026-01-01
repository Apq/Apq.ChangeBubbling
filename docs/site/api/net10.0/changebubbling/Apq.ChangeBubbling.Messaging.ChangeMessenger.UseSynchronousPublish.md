#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.UseSynchronousPublish Property

是否使用同步发布模式（直接调用 OnNext，不经过调度器）。
启用后可减少 Lambda 闭包分配和调度延迟，但会在调用线程上执行。
适用于不需要线程切换的高性能场景。

```csharp
public static bool UseSynchronousPublish { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')