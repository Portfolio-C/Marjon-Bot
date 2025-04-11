using MarjonBot.Application.Interfaces;

namespace MarjonBot.Services;
internal class BotManager()
{
    private readonly IReportService _service;

    public BotManager(IReportService service) : this()
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public async Task<MemoryStream> GenerateReportAsync()
    {
        var report = await _service.GenerateReportAsync();

        using var stream = new MemoryStream();

        await report.CopyToAsync(stream);
        stream.Position = 0;

        return stream;
    }
}
