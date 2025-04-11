using MarjonBot.Application.Interfaces;
using MarjonBot.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarjonBot.Application.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReportGenerator, ReportGenerator>();
        services.AddScoped<IReportService, ReportService>();
        return services;
    }
}
