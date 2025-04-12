using MarjonBot.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MarjonBot;

public class BotHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceProvider _serviceProvider;

    public BotHandler(ITelegramBotClient bot, IServiceProvider service)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _serviceProvider = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task OnMessage(Message msg, UpdateType type)
    {
        if (msg.Text is null)
        {
            return;
        }

        if (msg.Text.ToLower() == "/start")
        {
            await _bot.SendMessage(msg.Chat.Id, "Hello! I'm your bot. How can I assist you today?");
        }
        else if (msg.Text.ToLower() == "/report")
        {
            using var scope = _serviceProvider.CreateScope();
            var reportService = scope.ServiceProvider.GetService<IBotManager>();

            try
            {
                using var stream = await reportService!.GenerateReportAsync();

                if (stream.Length == 0)
                {
                    await _bot.SendMessage(msg.Chat.Id, "Hisobot fayli bo'sh. Iltimos, qayta urinib ko'ring.");
                    return;
                }
                await _bot.SendDocument(msg.Chat.Id, InputFile.FromStream(stream, $"report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
            }
            catch (Exception ex)
            {
                await _bot.SendMessage(msg.Chat.Id, $"Xatolik yuz berdi: {ex.Message}");
            }
        }
        else
        {
            await _bot.SendMessage(msg.Chat.Id, "salom!");
        }

    }
}
