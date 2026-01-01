#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging')

## BubblingChangeMessage Class

基于 CommunityToolkit\.Mvvm 的冒泡变更消息类型。
使用对象池减少高频场景下的 GC 压力。

```csharp
public sealed class BubblingChangeMessage : CommunityToolkit.Mvvm.Messaging.Messages.ValueChangedMessage<Apq.ChangeBubbling.Abstractions.BubblingChange>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; [CommunityToolkit\.Mvvm\.Messaging\.Messages\.ValueChangedMessage&lt;](https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.mvvm.messaging.messages.valuechangedmessage-1 'CommunityToolkit\.Mvvm\.Messaging\.Messages\.ValueChangedMessage\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.mvvm.messaging.messages.valuechangedmessage-1 'CommunityToolkit\.Mvvm\.Messaging\.Messages\.ValueChangedMessage\`1') &#129106; BubblingChangeMessage

| Methods | |
| :--- | :--- |
| [Rent\(BubblingChange\)](Apq.ChangeBubbling.Messaging.BubblingChangeMessage.Rent(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Messaging\.BubblingChangeMessage\.Rent\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 从对象池获取消息实例。 |
| [Return\(\)](Apq.ChangeBubbling.Messaging.BubblingChangeMessage.Return().md 'Apq\.ChangeBubbling\.Messaging\.BubblingChangeMessage\.Return\(\)') | 归还消息实例到对象池。 |
| [SetValue\(BubblingChange\)](Apq.ChangeBubbling.Messaging.BubblingChangeMessage.SetValue(Apq.ChangeBubbling.Abstractions.BubblingChange).md 'Apq\.ChangeBubbling\.Messaging\.BubblingChangeMessage\.SetValue\(Apq\.ChangeBubbling\.Abstractions\.BubblingChange\)') | 设置消息值（通过 IL Emit 生成的高性能委托）。 |
