using MarjonBot.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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

    public async Task OnUpdate(Update update, UpdateType type)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                if (update.Message is not null)
                    await OnMessage(update.Message);
                break;
            case UpdateType.CallbackQuery:
                if (update.CallbackQuery is not null)
                    await OnCallbackQuery(update.CallbackQuery);
                break;
        }

    }

    private async Task OnMessage(Message msg)
    {
        if (msg.Text.ToLower() == "/start")
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Reportlarni olish","get_reports")
                }
            });
            await _bot.SendMessage(msg.Chat.Id, "Assalomu aleykum! Reportlarni olish uchun quyidagi tugmani bosing", replyMarkup: inlineKeyboard);
        }
        else if (msg.Text.ToLower() == "/report")
        {
            await GetReports(msg.Chat.Id);
        }
        else
        {
            await _bot.SendMessage(msg.Chat.Id, "/start yoki /report ni bosing!");
        }
    }

    private async Task OnCallbackQuery(CallbackQuery callback)
    {
        if (callback.Data == "get_reports")
        {
            await GetReports(callback.Message!.Chat.Id);
        }
    }


    private async Task GetReports(long chatId)
    {
        using var scope = _serviceProvider.CreateScope();
        var reportService = scope.ServiceProvider.GetService<IBotManager>();

        try
        {
            using var stream = await reportService.GenerateReportAsync();
            if (stream.Length == 0)
            {
                await _bot.SendMessage(chatId, "Hisobot fayli bo'sh. Iltimos, qayta urinib ko'ring.");
                return;
            }

            await _bot.SendDocument(chatId, InputFile.FromStream(stream, $"report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
        }
        catch (Exception ex)
        {
            await _bot.SendMessage(chatId, $"Xatolik yuz berdi: {ex.Message}");
        }

    }
}
