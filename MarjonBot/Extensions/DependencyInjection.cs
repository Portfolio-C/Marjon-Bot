using MarjonBot.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace MarjonBot.Extensions;
internal static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddSingleton<ITelegramBotClient>(x => new TelegramBotClient(""));
        services.AddSingleton<BotHandler>();

        return services;
    }
}
