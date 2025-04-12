using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Interfaces;

public interface IReportGenerator
{
    public Task<Stream> GenerateAsync(List<Report> report);
}
