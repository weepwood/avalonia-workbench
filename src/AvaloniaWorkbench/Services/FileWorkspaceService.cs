using AvaloniaWorkbench.Models;

namespace AvaloniaWorkbench.Services;

public sealed class FileWorkspaceService
{
    public Task<IReadOnlyList<FileEntry>> ScanAsync(string folderPath, CancellationToken cancellationToken = default) =>
        Task.Run<IReadOnlyList<FileEntry>>(() =>
        {
            var directory = new DirectoryInfo(folderPath);
            if (!directory.Exists)
            {
                return [];
            }

            var entries = new List<FileEntry>();
            try
            {
                foreach (var item in directory.EnumerateFileSystemInfos().Take(2_000))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (item is DirectoryInfo childDirectory)
                    {
                        entries.Add(new FileEntry(
                            childDirectory.Name,
                            childDirectory.FullName,
                            "文件夹",
                            0,
                            childDirectory.LastWriteTime,
                            true));
                    }
                    else if (item is FileInfo file)
                    {
                        entries.Add(new FileEntry(
                            file.Name,
                            file.FullName,
                            string.IsNullOrWhiteSpace(file.Extension) ? "文件" : file.Extension.TrimStart('.').ToUpperInvariant(),
                            file.Length,
                            file.LastWriteTime,
                            false));
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return [];
            }
            catch (IOException)
            {
                return [];
            }

            return entries
                .OrderByDescending(entry => entry.IsDirectory)
                .ThenBy(entry => entry.Name, StringComparer.CurrentCultureIgnoreCase)
                .ToArray();
        }, cancellationToken);
}
