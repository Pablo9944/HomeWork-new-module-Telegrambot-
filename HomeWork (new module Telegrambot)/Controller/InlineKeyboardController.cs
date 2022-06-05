using HomeWork__new_module_Telegrambot_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HomeWork__new_module_Telegrambot_.Controller
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private static CallbackQuery callback;

        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
            callback = new CallbackQuery();


        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            else if (callbackQuery.Data == "Информация")
            {
                callback = callbackQuery;
                // Обновление пользовательской сессии новыми данными
                // _memoryStorage.GetSession(callbackQuery.From.Id).Choice = callbackQuery.Data;
                 callback = callbackQuery;
                 await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Посчитаем колличество символов в предложении \nВведите предложение: ", cancellationToken: ct, parseMode: ParseMode.Html);
            }
            else if (callbackQuery.Data == "Сумма")
            {
                callback = callbackQuery;
                // Обновление пользовательской сессии новыми данными
                _memoryStorage.GetSession(callbackQuery.From.Id).Choice = callbackQuery.Data;
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Получим сумму ваших чисел \nВведите числа через пробел: ", cancellationToken: ct, parseMode: ParseMode.Html);
            }
           
        }
        public static CallbackQuery GetCallbackQuery()
        {
            return callback;
        }
      
    }
}
