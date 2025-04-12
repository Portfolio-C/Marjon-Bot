// WeeklyReport.cs
using MarjonBot.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MarjonBot;

public class WeeklyReport(ITelegramBotClient botClient, IServiceProvider serviceProvider)
{
    private readonly long _chatId = 0123456789;

    public void Start()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                var now = DateTime.Now;

                if (now.DayOfWeek == DayOfWeek.Saturday)
                {
                    using var scope = serviceProvider.CreateScope();
                    var reportService = scope.ServiceProvider.GetService<IBotManager>();

                    try
                    {
                        using var stream = await reportService!.GenerateReportAsync();

                        if (stream.Length == 0)
                        {
                            await botClient.SendMessage(_chatId, "Hisobot fayli bo'sh. Iltimos, qayta urinib ko'ring.");
                            return;
                        }
                        await botClient.SendDocument(_chatId, InputFile.FromStream(stream, "weekly_report.xlsx"));
                    }
                    catch (Exception ex)
                    {
                        await botClient.SendMessage(_chatId, $"Xatolik yuz berdi: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(60));
            }
        });
    }
}
