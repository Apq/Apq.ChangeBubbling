#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.AsObservable\(string\) Method

获取指定环境的原始事件流。

```csharp
public static System.IObservable<Apq.ChangeBubbling.Abstractions.BubblingChange> AsObservable(string envName="default");
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsObservable(string).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.IObservable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')