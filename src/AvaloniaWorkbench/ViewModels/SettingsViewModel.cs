using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool restoreLastPage = true;

    [ObservableProperty]
    private bool showStatusBar = true;

    [ObservableProperty]
    private bool enableAnimations = true;

    [ObservableProperty]
    private string language = "简体中文";

    public IReadOnlyList<string> Languages { get; } = ["简体中文", "English"];
}
