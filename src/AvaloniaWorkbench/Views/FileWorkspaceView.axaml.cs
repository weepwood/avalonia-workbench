using Avalonia.Controls;
using Avalonia.Platform.Storage;
using AvaloniaWorkbench.ViewModels;

namespace AvaloniaWorkbench.Views;

public sealed partial class FileWorkspaceView : UserControl
{
    public FileWorkspaceView() => InitializeComponent();

    private async void SelectFolder_OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider is null)
        {
            return;
        }

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "选择要扫描的文件夹",
            AllowMultiple = false
        });

        if (folders.Count > 0 && DataContext is FileWorkspaceViewModel viewModel)
        {
            await viewModel.LoadFolderAsync(folders[0].Path.LocalPath);
        }
    }
}
