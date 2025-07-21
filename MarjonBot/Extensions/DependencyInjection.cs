using MarjonBot.Application.Extensions;
using MarjonBot.Application.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace MarjonBot.Extensions;

internal static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);
        // Load configuration from appsettings.json
        string token = configuration["TelegramBot:Token"]
            ?? throw new ArgumentNullException("Telegram Bot Token is not configured.");

        services.AddApplication();
        services.AddSingleton<ITelegramBotClient>(x => new TelegramBotClient(token));
        services.AddSingleton<BotHandler>();

        return services;
    }
}
