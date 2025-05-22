using MarjonBot;
using MarjonBot.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

internal class Program
{
    private static async Task Main(string[] args)
    {

        var services = new ServiceCollection();
        var serviceProvider = services
            .ConfigureServices()
            .BuildServiceProvider();

        var botHandler = serviceProvider.GetService<BotHandler>();
        var bot = serviceProvider.GetService<ITelegramBotClient>();

        bot.StartReceiving(
            updateHandler: async (botClient, update, cancellationToken) =>
            {
                if (update is not null)
                {
                    await botHandler.OnUpdate(update, update.Type);
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