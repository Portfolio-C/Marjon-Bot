using MarjonBot.Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MarjonBot.Application.Jobs;

public sealed class WeeklyReportJob(IBotManager botManager, ITelegramBotClient botClient)
{
    public async Task SendWeeklyJobAsync()
    {
        List<long> userIds = [123456789, 987654321]; // userIds should be fetched from a database or configuration

        foreach (var userId in userIds)
        {
            using var report = await botManager.GenerateReportAsync(userId);

            if (report.Length == 0)
            {
                await botClient.SendMessage(userId, "Hisobot fayli bo'sh. Iltimos, qayta urinib ko'ring.");
                continue;
            }

            await botClient.SendDocument(
                userId,
                InputFile.FromStream(report, $"weekly_report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"),
                "Bu sizning haftalik hisobotingiz");
        }
    }
}
