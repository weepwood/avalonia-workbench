using System.Collections.ObjectModel;
using AvaloniaWorkbench.Models;
using AvaloniaWorkbench.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaWorkbench.ViewModels;

public sealed partial class DataLabViewModel : ViewModelBase
{
    private readonly DataLabService service = new();
    private IReadOnlyList<SampleRecord> source = [];

    public DataLabViewModel()
    {
        Generate(2_500);
    }

    public ObservableCollection<SampleRecord> Records { get; } = [];

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private string statusText = "全部状态";

    public IReadOnlyList<string> StatusOptions { get; } = ["全部状态", "正常", "处理中", "需关注", "已归档"];
    public string ResultSummary => $"当前显示 {Records.Count:N0} 条，共 {source.Count:N0} 条";

    partial void OnSearchTextChanged(string value) => ApplyFilter();
    partial void OnStatusTextChanged(string value) => ApplyFilter();

    [RelayCommand]
    private void GenerateMore() => Generate(source.Count >= 10_000 ? 2_500 : source.Count + 2_500);

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        StatusText = "全部状态";
    }

    private void Generate(int count)
    {
        source = service.Generate(count);
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        var query = source.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var term = SearchText.Trim();
            query = query.Where(record =>
                record.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                record.Category.Contains(term, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.Equals(StatusText, "全部状态", StringComparison.Ordinal))
        {
            query = query.Where(record => string.Equals(record.Status, StatusText, StringComparison.Ordinal));
        }

        Records.Clear();
        foreach (var record in query.Take(10_000))
        {
            Records.Add(record);
        }

        OnPropertyChanged(nameof(ResultSummary));
    }
}
