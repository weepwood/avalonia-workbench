using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class ThemeLabViewModel : ViewModelBase
{
    [ObservableProperty]
    private string selectedTheme = "跟随系统";

    [ObservableProperty]
    private bool compactMode;

    [ObservableProperty]
    private double sampleProgress = 68;

    [RelayCommand]
    private void SetTheme(string? theme)
    {
        if (Application.Current is null || string.IsNullOrWhiteSpace(theme))
        {
            return;
        }

        SelectedTheme = theme;
        Application.Current.RequestedThemeVariant = theme switch
        {
            "浅色" => ThemeVariant.Light,
            "深色" => ThemeVariant.Dark,
            _ => ThemeVariant.Default
        };
    }

    [RelayCommand]
    private void IncrementProgress() => SampleProgress = SampleProgress >= 100 ? 0 : SampleProgress + 8;
}
