#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.RegisterFilter\(string, IChangeEventFilter\) Method

注册事件过滤器。

```csharp
public static void RegisterFilter(string envName, Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter filter);
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterFilter(string,Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

环境名称

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.RegisterFilter(string,Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter).filter'></a>

`filter` [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')

事件过滤器