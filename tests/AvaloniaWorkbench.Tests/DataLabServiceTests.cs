using AvaloniaWorkbench.Services;

namespace AvaloniaWorkbench.Tests;

public sealed class DataLabServiceTests
{
    [Fact]
    public void Generate_ReturnsRequestedNumberOfRecords()
    {
        var service = new DataLabService();

        var records = service.Generate(250);

        Assert.Equal(250, records.Count);
        Assert.Equal(1, records[0].Id);
        Assert.Equal(250, records[^1].Id);
    }

    [Fact]
    public void Generate_IsDeterministicForDemoData()
    {
        var service = new DataLabService();

        var first = service.Generate(10);
        var second = service.Generate(10);

        Assert.Equal(first.Select(item => item.Score), second.Select(item => item.Score));
        Assert.Equal(first.Select(item => item.Status), second.Select(item => item.Status));
    }
}
