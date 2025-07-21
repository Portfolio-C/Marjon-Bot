using MarjonBot.Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MarjonBot.Application.Handlers;

public class BotHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceProvider _serviceProvider;
    private readonly MessageHandler _messageHandler;
    private readonly CallbackQueryHandler _callbackQueryHandler;
    private readonly IApiService _apiService;

    public BotHandler(ITelegramBotClient bot, IServiceProvider service, IApiService apiService)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _serviceProvider = service ?? throw new ArgumentNullException(nameof(service));
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _messageHandler = new MessageHandler(_bot, _serviceProvider, _apiService);
        _callbackQueryHandler = new CallbackQueryHandler(_bot, _serviceProvider);
    }

    public async Task OnUpdate(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                if (update.Message is not null)
                {
                    await _messageHandler.HandleAsync(update.Message);
                }
                break;
            case UpdateType.CallbackQuery:
                if (update.CallbackQuery is not null)
                {
                    await _callbackQueryHandler.HandleAsync(update.CallbackQuery);
                }
                break;
        }
    }
}
