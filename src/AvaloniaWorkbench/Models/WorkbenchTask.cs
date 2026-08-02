using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.Models;

public sealed partial class WorkbenchTask : ObservableObject
{
    public WorkbenchTask(string name, string detail, Action<WorkbenchTask> cancel)
    {
        Id = Guid.NewGuid();
        Name = name;
        Detail = detail;
        StartedAt = DateTime.Now;
        CancelCommand = new RelayCommand(() => cancel(this));
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Detail { get; }
    public DateTime StartedAt { get; }
    public IRelayCommand CancelCommand { get; }

    [ObservableProperty]
    private double progress;

    [ObservableProperty]
    private string status = "等待中";

    [ObservableProperty]
    private bool canCancel = true;
}
