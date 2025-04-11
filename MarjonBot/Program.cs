using MarjonBot.Extensions;
using MarjonBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.ConfigureServices()
            .BuildServiceProvider();


        using var cts = new CancellationTokenSource();
        var bot = new TelegramBotClient("", cancellationToken: cts.Token);
        var me = await bot.GetMe();
        bot.OnMessage += OnMessage;

        Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
        Console.ReadLine();
        cts.Cancel();

        async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Text is null)
            {
                return;
            }

            if (msg.Text.ToLower() == "/start")
            {
                await bot.SendMessage(msg.Chat.Id, "Hello! I'm your bot. How can I assist you today?");
            }
            else if (msg.Text.ToLower() == "/report")
            {
                var reportService = new BotManager();

                var stream = await reportService.GenerateReportAsync();

                await bot.SendDocument(msg.Chat.Id, InputFile.FromStream(stream, "report.xlsx"));
            }
            else
            {
                await bot.SendMessage(msg.Chat.Id, "I didn't understand that command.");
            }
        }
    }
}