#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core').[ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase')

## ChangeNodeBase\.InvalidateDepthCache\(\) Method

使深度缓存失效（使用迭代避免深层树结构的栈溢出）。
使用 ThreadStatic 缓存的 Stack 避免每次调用都分配新对象。

```csharp
private void InvalidateDepthCache();
```