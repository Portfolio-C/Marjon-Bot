using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Interfaces;

public interface IReportGenerator
{
    /// Generates a report based on the provided list of reports.
    public Task<Stream> GenerateAsync(List<Report> report);
}
