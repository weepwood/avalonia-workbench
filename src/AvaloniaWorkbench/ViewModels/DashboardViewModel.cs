using System.Collections.ObjectModel;
using AvaloniaWorkbench.Models;

namespace AvaloniaWorkbench.ViewModels;

public sealed class DashboardViewModel : ViewModelBase
{
    public DashboardViewModel()
    {
        Activities =
        [
            new("工作台启动", "跨平台桌面环境已完成初始化", DateTime.Now, "系统"),
            new("数据实验室就绪", "已生成 2,500 条可筛选演示数据", DateTime.Now.AddMinutes(-2), "数据"),
            new("任务引擎就绪", "支持并发、进度反馈与取消操作", DateTime.Now.AddMinutes(-4), "任务"),
            new("诊断信息已采集", "运行环境和架构信息可随时刷新", DateTime.Now.AddMinutes(-6), "诊断")
        ];
    }

    public ObservableCollection<ActivityEntry> Activities { get; }
    public string RuntimeLabel => $"{Environment.OSVersion.Platform} · .NET {Environment.Version.Major}";
}
