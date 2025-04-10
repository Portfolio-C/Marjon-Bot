namespace MarjonBot.Application.Interfaces;

public interface IReportService
{
    public Task<MemoryStream> GenerateReportAsync();
}
