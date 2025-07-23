using MarjonBot.Domain.Entities;

namespace MarjonBot.Application.Interfaces;

public interface IApiService
{
    Task<bool> LoginAsync(LoginDto request);
    Task<List<Report>> GetReports(long userId);
}
