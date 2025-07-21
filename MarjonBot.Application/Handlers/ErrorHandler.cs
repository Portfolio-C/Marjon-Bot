using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace MarjonBot.Application.Handlers;
public static class ErrorHandler
{
    public static Task HandleAsync(
       ITelegramBotClient botClient,
       Exception exception,
       CancellationToken token)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiEx => $"[Telegram API Xato] Kod: {apiEx.ErrorCode}\nXabar: {apiEx.Message}",
            _ => $"[Umumiy Xato] {exception.Message}"
        };

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ Xatolik yuz berdi: {errorMessage}");
        Console.ResetColor();

        return Task.CompletedTask;
    }
}
