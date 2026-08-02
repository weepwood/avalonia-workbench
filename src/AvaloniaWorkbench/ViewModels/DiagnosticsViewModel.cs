using System.Collections.ObjectModel;
using AvaloniaWorkbench.Models;
using AvaloniaWorkbench.Services;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class DiagnosticsViewModel : ViewModelBase
{
    private readonly DiagnosticsService service = new();

    public DiagnosticsViewModel() => Refresh();

    public ObservableCollection<DiagnosticItem> Items { get; } = [];
    public string CapturedAt { get; private set; } = string.Empty;

    [RelayCommand]
    private void Refresh()
    {
        Items.Clear();
        foreach (var item in service.Capture())
        {
            Items.Add(new DiagnosticItem(item.Key, item.Value));
        }

        CapturedAt = $"采集时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        OnPropertyChanged(nameof(CapturedAt));
    }
}
