using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarjonBot.Application.Handlers;

internal sealed class MessageHandler(ITelegramBotClient botClient, IServiceProvider serviceProvider)
{
    public async Task HandleAsync(Message message)
    {
        if (message.Text == null)
        {
            return;
        }

        var chatId = message.Chat.Id;
        var text = message.Text;

        switch (text.ToLower())
        {
            case "/start":
                await StartCommand(chatId, botClient);
                break;
            case "/report":
                await GetReport.GetReportCommand(chatId, botClient, serviceProvider);
                break;
            default:
                await OnDefaultMessage(chatId, botClient);
                break;
        }
    }

    private static Task<Message> StartCommand(long chatId, ITelegramBotClient client)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
           {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Reportlarni olish","get_reports")
                }
            });

        return client.SendMessage(
            chatId,
            "Assalomu alaykum! Reportlarni olish uchun quyidagi tugmani bosing",
            replyMarkup: inlineKeyboard);
    }

    private static Task<Message> OnDefaultMessage(long chatId, ITelegramBotClient client)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
          {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Reportlarni olish","get_reports")
                }
            });

        return client.SendMessage(
            chatId,
            "Marjon reports — Sizga hisobotlarni tez va aniq taqdim etamiz.",
            replyMarkup: inlineKeyboard);
    }
}
