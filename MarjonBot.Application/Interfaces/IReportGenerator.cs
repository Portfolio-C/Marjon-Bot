using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Interfaces;

public interface IReportGenerator
{
    public MemoryStream Generate(List<Report> report);
}
