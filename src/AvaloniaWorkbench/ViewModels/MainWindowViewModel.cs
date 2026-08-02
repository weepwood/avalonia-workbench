using System.Collections.ObjectModel;
using Avalonia.Threading;
using AvaloniaWorkbench.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase
{
    private readonly DispatcherTimer clockTimer;

    public MainWindowViewModel()
    {
        NavigationItems =
        [
            new("dashboard", "仪表盘", "⌂", "运行概况与最近活动"),
            new("data", "数据实验室", "▦", "表格、筛选与虚拟化"),
            new("files", "文件工作区", "▣", "本地目录扫描与管理"),
            new("tasks", "任务中心", "◴", "异步任务、进度与取消"),
            new("theme", "UI 实验室", "◈", "主题、控件与交互状态"),
            new("diagnostics", "诊断中心", "◎", "运行环境与应用信息"),
            new("settings", "设置", "⚙", "应用行为和偏好")
        ];

        SelectedNavigation = NavigationItems[0];
        ClockText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        clockTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Background, (_, _) =>
        {
            ClockText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        });
        clockTimer.Start();
    }

    public ObservableCollection<NavigationItem> NavigationItems { get; }
    public DashboardViewModel Dashboard { get; } = new();
    public DataLabViewModel DataLab { get; } = new();
    public FileWorkspaceViewModel FileWorkspace { get; } = new();
    public TaskCenterViewModel TaskCenter { get; } = new();
    public ThemeLabViewModel ThemeLab { get; } = new();
    public DiagnosticsViewModel Diagnostics { get; } = new();
    public SettingsViewModel Settings { get; } = new();

    [ObservableProperty]
    private NavigationItem? selectedNavigation;

    [ObservableProperty]
    private string clockText = string.Empty;

    public string PageTitle => SelectedNavigation?.Title ?? "工作台";
    public string PageDescription => SelectedNavigation?.Description ?? string.Empty;
    public bool IsDashboard => IsSelected("dashboard");
    public bool IsDataLab => IsSelected("data");
    public bool IsFileWorkspace => IsSelected("files");
    public bool IsTaskCenter => IsSelected("tasks");
    public bool IsThemeLab => IsSelected("theme");
    public bool IsDiagnostics => IsSelected("diagnostics");
    public bool IsSettings => IsSelected("settings");

    partial void OnSelectedNavigationChanged(NavigationItem? value)
    {
        OnPropertyChanged(nameof(PageTitle));
        OnPropertyChanged(nameof(PageDescription));
        OnPropertyChanged(nameof(IsDashboard));
        OnPropertyChanged(nameof(IsDataLab));
        OnPropertyChanged(nameof(IsFileWorkspace));
        OnPropertyChanged(nameof(IsTaskCenter));
        OnPropertyChanged(nameof(IsThemeLab));
        OnPropertyChanged(nameof(IsDiagnostics));
        OnPropertyChanged(nameof(IsSettings));
    }

    private bool IsSelected(string key) => string.Equals(SelectedNavigation?.Key, key, StringComparison.Ordinal);
}
