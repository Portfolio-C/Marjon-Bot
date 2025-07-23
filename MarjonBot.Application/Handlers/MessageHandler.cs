using MarjonBot.Application.Interfaces;
using MarjonBot.Domain.Entities;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarjonBot.Application.Handlers;

internal sealed class MessageHandler(ITelegramBotClient botClient, IServiceProvider serviceProvider, IApiService apiService)
{
    private static readonly HashSet<long> _logginedUsers = new();
    public async Task HandleAsync(Message message)
    {
        if (message.Text == null)
        {
            return;
        }

        var chatId = message.Chat.Id;
        var text = message.Text;

        if (text.Contains('\n'))
        {
            await Login(chatId, text, botClient);
            return;
        }


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

    private Task<Message> StartCommand(long chatId, ITelegramBotClient client)
    {
        if (!_logginedUsers.Contains(chatId))
        {
            return client.SendMessage(
                chatId,
"                👋 Assalomu alaykum! Tizimga kirish uchun quyidagicha formatda telefon raqamingiz va parolingizni yuboring:\n📱 +998994566543\n🔐 sizning_parolingiz");

        }

        var inlineKeyboard = new InlineKeyboardMarkup(
           [
                [
                    InlineKeyboardButton.WithCallbackData("📊 Hisobotlarni yuklab olish","get_reports")
                ]
            ]);

        return client.SendMessage(
            chatId,
            "Assalomu alaykum! 📊 Hisobotlarni yuklab olish uchun quyidagi tugmani bosing",
            replyMarkup: inlineKeyboard);
    }

    private static Task<Message> OnDefaultMessage(long chatId, ITelegramBotClient client)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(
          [
                [
                    InlineKeyboardButton.WithCallbackData("📊 Hisobotlarni yuklab olish","get_reports")
                ]
            ]);

        return client.SendMessage(
            chatId,
            "Marjon reports — Sizga hisobotlarni tez va aniq taqdim etamiz.",
            replyMarkup: inlineKeyboard);
    }

    private async Task<Message> Login(long chatId, string text, ITelegramBotClient client)
    {
        var loginRequest = text.Split('\n');
        var isValidPhoneNumber = Regex.IsMatch(loginRequest[0], @"^\+998\d{9}$");

        if (!isValidPhoneNumber)
        {
            return await client.SendMessage(
                chatId,
                "❗ Telefon raqam noto‘g‘ri formatda yuborildi.\n\nIltimos, quyidagi ko‘rinishda qaytadan yuboring:\n📱 +998991234567\n🔑 Parolingizni ham birga yuboring.");
        }

        var request = new LoginDto()
        {
            PhoneNumber = loginRequest[0],
            Password = loginRequest[1]
        };

        var result = await apiService.LoginAsync(request);
        if (result)
        {
            _logginedUsers.Add(chatId);

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("📊 Hisobotlarni olish", "get_reports") }
            });

            return await client.SendMessage(
                chatId,
                "✅ Tizimga muvaffaqiyatli kirdingiz. Endi hisobotlarni olish uchun quyidagi tugmadan foydalaning.",
                replyMarkup: inlineKeyboard);
        }

        return await client.SendMessage(
            chatId,
            "⚠️ Sizning profilingiz tizimda topilmadi. Iltimos, avval ro‘yxatdan o‘ting yoki admin bilan bog‘laning.");
    }
}
