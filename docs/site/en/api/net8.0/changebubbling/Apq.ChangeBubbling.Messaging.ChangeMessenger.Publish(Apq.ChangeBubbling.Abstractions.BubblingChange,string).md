#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.Publish\(BubblingChange, string\) Method

发布冒泡事件到指定环境（未注册则落到默认）。

```csharp
public static void Publish(Apq.ChangeBubbling.Abstractions.BubblingChange change, string envName="default");
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.Publish(Apq.ChangeBubbling.Abstractions.BubblingChange,string).change'></a>

`change` [BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.Publish(Apq.ChangeBubbling.Abstractions.BubblingChange,string).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')