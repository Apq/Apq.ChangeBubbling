namespace Apq.ChangeBubbling.Tests;

/// <summary>
/// 定义测试集合，确保使用共享静态资源（如 ChangeMessenger）的测试串行运行。
/// </summary>
[CollectionDefinition("Sequential")]
public class SequentialCollection : ICollectionFixture<SequentialTestFixture>
{
}

/// <summary>
/// 测试夹具，用于在测试集合开始和结束时执行初始化/清理。
/// </summary>
public class SequentialTestFixture : IDisposable
{
    public SequentialTestFixture()
    {
        // 测试集合开始时重置 ChangeMessenger
        Messaging.ChangeMessenger.Reset();
    }

    public void Dispose()
    {
        // 测试集合结束时重置 ChangeMessenger
        Messaging.ChangeMessenger.Reset();
    }
}
