using MarjonBot.Application.Interfaces;

namespace MarjonBot.Application.Services;

internal sealed class BotManager(IReportService reportService) : IBotManager
{
    public async Task<MemoryStream> GenerateReportAsync(long userId)
    {
        var report = await reportService.GenerateReportAsync(userId);
        var stream = new MemoryStream();

        await report.CopyToAsync(stream);
        stream.Position = 0;

        return stream;
    }
}
