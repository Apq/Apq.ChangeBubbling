#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core')

## IChangeNode Interface

定义具有父子关系、支持冒泡通知的节点接口。

```csharp
public interface IChangeNode : Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier, System.ComponentModel.INotifyPropertyChanged
```

Derived  
&#8627; [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

Implements [IBubblingChangeNotifier](Apq.ChangeBubbling.Abstractions.IBubblingChangeNotifier.md 'Apq\.ChangeBubbling\.Abstractions\.IBubblingChangeNotifier'), [System\.ComponentModel\.INotifyPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged 'System\.ComponentModel\.INotifyPropertyChanged')

| Properties | |
| :--- | :--- |
| [Children](Apq.ChangeBubbling.Core.IChangeNode.Children.md 'Apq\.ChangeBubbling\.Core\.IChangeNode\.Children') | 子节点只读集合。 |
| [Name](Apq.ChangeBubbling.Core.IChangeNode.Name.md 'Apq\.ChangeBubbling\.Core\.IChangeNode\.Name') | 节点名称，用于路径标识。 |
| [Parent](Apq.ChangeBubbling.Core.IChangeNode.Parent.md 'Apq\.ChangeBubbling\.Core\.IChangeNode\.Parent') | 父节点，根节点为 null。 |

| Methods | |
| :--- | :--- |
| [AttachChild\(IChangeNode\)](Apq.ChangeBubbling.Core.IChangeNode.AttachChild(Apq.ChangeBubbling.Core.IChangeNode).md 'Apq\.ChangeBubbling\.Core\.IChangeNode\.AttachChild\(Apq\.ChangeBubbling\.Core\.IChangeNode\)') | 附加子节点并建立事件订阅与冒泡转译。 |
| [DetachChild\(IChangeNode\)](Apq.ChangeBubbling.Core.IChangeNode.DetachChild(Apq.ChangeBubbling.Core.IChangeNode).md 'Apq\.ChangeBubbling\.Core\.IChangeNode\.DetachChild\(Apq\.ChangeBubbling\.Core\.IChangeNode\)') | 移除子节点并解除订阅。 |
