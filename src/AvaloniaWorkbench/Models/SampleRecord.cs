namespace AvaloniaWorkbench.Models;

public sealed record SampleRecord(
    int Id,
    string Name,
    string Category,
    string Status,
    double Score,
    DateTime UpdatedAt);
