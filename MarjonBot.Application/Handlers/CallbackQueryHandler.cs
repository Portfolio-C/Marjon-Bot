using Telegram.Bot;
using Telegram.Bot.Types;

namespace MarjonBot.Application.Handlers;
internal sealed class CallbackQueryHandler(ITelegramBotClient botClient, IServiceProvider serviceProvider)
{
    public async Task HandleAsync(CallbackQuery callbackQuery)
    {
        if (callbackQuery.Data is null)
        {
            return;
        }

        var chatId = callbackQuery.Message!.Chat.Id;

        if (callbackQuery.Data == "get_reports")
        {
            await GetReport.GetReportCommand(chatId, botClient, serviceProvider);
        }
    }
}
