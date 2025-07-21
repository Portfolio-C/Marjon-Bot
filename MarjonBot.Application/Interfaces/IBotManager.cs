namespace MarjonBot.Application.Interfaces;

public interface IBotManager
{
    Task<MemoryStream> GenerateReportAsync(long userId);
}
