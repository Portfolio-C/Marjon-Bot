using MarjonBot.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MarjonBot.Application.Handlers;
internal static class GetReport
{
    public static async Task GetReportCommand(long chatId, ITelegramBotClient botClient, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var reportService = scope.ServiceProvider.GetService<IBotManager>();

        try
        {
            if (reportService is null)
            {
                await botClient.SendMessage(chatId, "Hisobot xizmati topilmadi. Iltimos, administrator bilan bog'laning.");
                return;
            }
            using var stream = await reportService.GenerateReportAsync(chatId);
            if (stream.Length == 0)
            {
                await botClient.SendMessage(chatId, "Hisobot fayli bo'sh. Iltimos, qayta urinib ko'ring.");
                return;
            }

            await botClient.SendDocument(chatId, InputFile.FromStream(stream, $"report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();

            await botClient.SendMessage(chatId, $"Xatolik yuz berdi");
        }
    }
}
