using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using AvaloniaWorkbench.Models;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class TaskCenterViewModel : ViewModelBase
{
    private readonly ConcurrentDictionary<Guid, CancellationTokenSource> cancellationSources = new();
    private int taskNumber;

    public ObservableCollection<WorkbenchTask> Tasks { get; } = [];
    public int ActiveCount => Tasks.Count(task => task.Status is "等待中" or "运行中");
    public int CompletedCount => Tasks.Count(task => task.Status == "已完成");

    [RelayCommand]
    private async Task StartTaskAsync()
    {
        var number = Interlocked.Increment(ref taskNumber);
        var task = new WorkbenchTask($"模拟任务 {number:00}", "扫描文件、计算摘要并生成索引", CancelTask);
        var cancellation = new CancellationTokenSource();
        cancellationSources[task.Id] = cancellation;
        Tasks.Insert(0, task);
        NotifyCounters();

        try
        {
            task.Status = "运行中";
            for (var step = 1; step <= 100; step++)
            {
                await Task.Delay(45, cancellation.Token);
                task.Progress = step;
            }

            task.Status = "已完成";
            task.CanCancel = false;
        }
        catch (OperationCanceledException)
        {
            task.Status = "已取消";
            task.CanCancel = false;
        }
        finally
        {
            cancellationSources.TryRemove(task.Id, out _);
            cancellation.Dispose();
            NotifyCounters();
        }
    }

    private void CancelTask(WorkbenchTask task)
    {
        if (cancellationSources.TryGetValue(task.Id, out var source))
        {
            source.Cancel();
        }
    }

    [RelayCommand]
    private void ClearFinished()
    {
        var finished = Tasks.Where(task => task.Status is "已完成" or "已取消").ToArray();
        foreach (var task in finished)
        {
            Tasks.Remove(task);
        }

        NotifyCounters();
    }

    private void NotifyCounters()
    {
        OnPropertyChanged(nameof(ActiveCount));
        OnPropertyChanged(nameof(CompletedCount));
    }
}
