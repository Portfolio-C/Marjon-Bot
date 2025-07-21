using Hangfire;
using Hangfire.MemoryStorage;
using MarjonBot.Application.Extensions;
using MarjonBot.Application.Handlers;
using MarjonBot.Application.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureAppConfiguration(config =>
     {
         config.SetBasePath(Directory.GetCurrentDirectory());
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
     })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null");
        }

        services.AddApplication();

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(configuration["TelegramBot:Token"]!));
        services.AddSingleton<BotHandler>();

        services.AddHangfire(cfg => cfg.UseMemoryStorage());
        services.AddHangfireServer();
    })
    .Build();


// Register Syncfusion license
var configuration = host.Services.GetRequiredService<IConfiguration>();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion:LicenseKey"]);

// Resolve services
var bot = host.Services.GetRequiredService<ITelegramBotClient>();
var botHandler = host.Services.GetRequiredService<BotHandler>();
var weeklyReport = host.Services.GetRequiredService<WeeklyReportJob>();

var recurringJobManager = host.Services.GetRequiredService<IRecurringJobManager>();

// Configure Hangfire
RecurringJob.AddOrUpdate(
    "weekly-report",
    () => weeklyReport.SendWeeklyJobAsync(),
    Cron.Minutely());

// Start the bot
bot!.StartReceiving(
    updateHandler: async (botClient, update, cancellationToken) =>
    {
        if (update != null)
            await botHandler.OnUpdate(update);
    },
    errorHandler: ErrorHandler.HandleAsync
);

Console.WriteLine("Bot ishga tushdi...");
await host.RunAsync();
await Task.Delay(-1);