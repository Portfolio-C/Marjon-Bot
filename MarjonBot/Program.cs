using MarjonBot;
using MarjonBot.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");

        var services = new ServiceCollection();
        var serviceProvider = services
            .ConfigureServices()
            .BuildServiceProvider();

        var botHandler = serviceProvider.GetService<BotHandler>();
        var bot = serviceProvider.GetService<ITelegramBotClient>();

        bot.StartReceiving(
            updateHandler: async (botClient, update, cancellationToken) =>
            {
                if (update.Message is { } message)
                {
                    await botHandler.OnMessage(message, update.Type);
                }
            },
            errorHandler: async (botHandler, exceprion, cancellationToken) =>
            {
                Console.WriteLine($"Xato: {exceprion.Message}");
            });


        Console.WriteLine("Bot ishga tushdi...");
        await Task.Delay(-1);
    }
}