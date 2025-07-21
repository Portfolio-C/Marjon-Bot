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

    public BotHandler(ITelegramBotClient bot, IServiceProvider service)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _serviceProvider = service ?? throw new ArgumentNullException(nameof(service));
        _messageHandler = new MessageHandler(_bot, _serviceProvider);
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
