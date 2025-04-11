using MarjonBot.Application.Interfaces;

namespace MarjonBot.Application.Services;

internal sealed class BotManager : IBotManager
{
    private readonly IReportService _reportService;

    public BotManager(IReportService reportService)
    {
        _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
    }

    public async Task<MemoryStream> GenerateReportAsync()
    {
        var report = await _reportService.GenerateReportAsync();
        var stream = new MemoryStream();

        await report.CopyToAsync(stream);
        stream.Position = 0;

        return stream;
    }
}
