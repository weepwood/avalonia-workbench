namespace AvaloniaWorkbench.Models;

public sealed record FileEntry(
    string Name,
    string FullPath,
    string Kind,
    long Size,
    DateTime ModifiedAt,
    bool IsDirectory)
{
    public string DisplaySize => IsDirectory ? "—" : FormatBytes(Size);

    private static string FormatBytes(long bytes)
    {
        string[] units = ["B", "KB", "MB", "GB", "TB"];
        double value = bytes;
        var unit = 0;
        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }

        return $"{value:0.##} {units[unit]}";
    }
}
