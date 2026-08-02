using AvaloniaWorkbench.Models;

namespace AvaloniaWorkbench.Services;

public sealed class DataLabService
{
    private static readonly string[] Categories = ["系统工具", "文件管理", "数据处理", "网络服务", "开发效率"];
    private static readonly string[] Statuses = ["正常", "处理中", "需关注", "已归档"];

    public IReadOnlyList<SampleRecord> Generate(int count)
    {
        var random = new Random(20260802);
        var now = DateTime.Now;
        return Enumerable.Range(1, count)
            .Select(index => new SampleRecord(
                index,
                $"演示记录 {index:0000}",
                Categories[index % Categories.Length],
                Statuses[random.Next(Statuses.Length)],
                Math.Round(random.NextDouble() * 100, 1),
                now.AddMinutes(-random.Next(0, 120_000))))
            .ToArray();
    }
}
