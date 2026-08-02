using System.Runtime.InteropServices;

namespace AvaloniaWorkbench.Services;

public sealed class DiagnosticsService
{
    public IReadOnlyDictionary<string, string> Capture()
    {
        using var process = System.Diagnostics.Process.GetCurrentProcess();
        return new Dictionary<string, string>
        {
            ["应用版本"] = typeof(DiagnosticsService).Assembly.GetName().Version?.ToString(3) ?? "0.1.0",
            ["操作系统"] = RuntimeInformation.OSDescription,
            ["系统架构"] = RuntimeInformation.OSArchitecture.ToString(),
            ["进程架构"] = RuntimeInformation.ProcessArchitecture.ToString(),
            [".NET 运行时"] = RuntimeInformation.FrameworkDescription,
            ["处理器数量"] = Environment.ProcessorCount.ToString(),
            ["工作集"] = FormatBytes(Environment.WorkingSet),
            ["进程启动时间"] = process.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
            ["当前目录"] = AppContext.BaseDirectory,
            ["用户数据目录"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        };
    }

    private static string FormatBytes(long bytes) => $"{bytes / 1024d / 1024d:0.0} MB";
}
