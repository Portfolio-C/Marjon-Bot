using MarjonBot.Application.Interfaces;
using MarjonBot.Application.Jobs;
using MarjonBot.Application.Services;
using MarjonBot.Application.Services.Bot;
using MarjonBot.Application.Services.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace MarjonBot.Application.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReportGenerator, ReportGenerator>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IBotManager, BotManager>();
        services.AddScoped<IApiService, ApiService>();
        services.AddSingleton<WeeklyReportJob>();
        services.AddSingleton<HttpClient>();

        return services;
    }
}
