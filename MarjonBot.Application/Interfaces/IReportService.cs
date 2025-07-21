namespace MarjonBot.Application.Interfaces;

public interface IReportService
{
    public Task<Stream> GenerateReportAsync(long userId);
}
