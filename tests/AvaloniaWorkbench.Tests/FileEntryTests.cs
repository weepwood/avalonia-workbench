using AvaloniaWorkbench.Models;

namespace AvaloniaWorkbench.Tests;

public sealed class FileEntryTests
{
    [Theory]
    [InlineData(0, "0 B")]
    [InlineData(1024, "1 KB")]
    [InlineData(1048576, "1 MB")]
    public void DisplaySize_FormatsBytes(long size, string expected)
    {
        var entry = new FileEntry("demo.bin", "/tmp/demo.bin", "BIN", size, DateTime.Now, false);

        Assert.Equal(expected, entry.DisplaySize);
    }

    [Fact]
    public void DisplaySize_UsesDashForDirectories()
    {
        var entry = new FileEntry("folder", "/tmp/folder", "文件夹", 0, DateTime.Now, true);

        Assert.Equal("—", entry.DisplaySize);
    }
}
