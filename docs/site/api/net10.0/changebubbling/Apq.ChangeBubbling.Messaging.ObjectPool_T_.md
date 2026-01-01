#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging')

## ObjectPool\<T\> Class

高性能对象池实现，使用 ThreadLocal \+ 共享池模式。
参考 System\.Buffers\.ArrayPool 的设计，减少跨线程竞争。

```csharp
internal sealed class ObjectPool<T>
    where T : class
```
#### Type parameters

<a name='Apq.ChangeBubbling.Messaging.ObjectPool_T_.T'></a>

`T`

对象类型

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; ObjectPool\<T\>