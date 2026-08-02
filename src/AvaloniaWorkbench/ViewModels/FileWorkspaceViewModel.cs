using System.Collections.ObjectModel;
using AvaloniaWorkbench.Models;
using AvaloniaWorkbench.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class FileWorkspaceViewModel : ViewModelBase
{
    private readonly FileWorkspaceService service = new();
    private IReadOnlyList<FileEntry> source = [];

    public ObservableCollection<FileEntry> Entries { get; } = [];

    [ObservableProperty]
    private string currentFolder = "尚未选择文件夹";

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string message = "选择一个本地文件夹，或将文件夹拖入此区域。";

    public string Summary
    {
        get
        {
            var files = source.Count(entry => !entry.IsDirectory);
            var folders = source.Count(entry => entry.IsDirectory);
            var bytes = source.Where(entry => !entry.IsDirectory).Sum(entry => entry.Size);
            return $"{folders:N0} 个文件夹 · {files:N0} 个文件 · {bytes / 1024d / 1024d:0.##} MB";
        }
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    public async Task LoadFolderAsync(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            return;
        }

        IsBusy = true;
        Message = "正在扫描目录…";
        try
        {
            source = await service.ScanAsync(folderPath);
            CurrentFolder = folderPath;
            Message = source.Count == 0 ? "目录为空，或当前进程没有访问权限。" : "扫描完成";
            ApplyFilter();
            OnPropertyChanged(nameof(Summary));
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Clear()
    {
        source = [];
        Entries.Clear();
        CurrentFolder = "尚未选择文件夹";
        Message = "选择一个本地文件夹，或将文件夹拖入此区域。";
        OnPropertyChanged(nameof(Summary));
    }

    private void ApplyFilter()
    {
        var query = source.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            query = query.Where(entry => entry.Name.Contains(SearchText.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        Entries.Clear();
        foreach (var entry in query)
        {
            Entries.Add(entry);
        }
    }
}
