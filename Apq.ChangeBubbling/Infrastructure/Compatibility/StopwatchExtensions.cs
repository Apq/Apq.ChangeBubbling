using System.Diagnostics;

namespace Apq.ChangeBubbling.Infrastructure.Compatibility;

/// <summary>
/// 为 .NET 6 提供 Stopwatch.GetElapsedTime 的 polyfill。
/// .NET 7+ 已内置此方法。
/// </summary>
internal static class StopwatchExtensions
{
#if !NET7_0_OR_GREATER
    /// <summary>
    /// 获取从指定时间戳到当前时间的经过时间。
    /// </summary>
    /// <param name="startingTimestamp">起始时间戳（由 Stopwatch.GetTimestamp() 获取）。</param>
    /// <returns>经过的时间。</returns>
    public static TimeSpan GetElapsedTime(long startingTimestamp)
    {
        long endTimestamp = Stopwatch.GetTimestamp();
        long timestampDelta = endTimestamp - startingTimestamp;
        return new TimeSpan((long)(timestampDelta * ((double)TimeSpan.TicksPerSecond / Stopwatch.Frequency)));
    }
#endif
}
