using HomeWork__new_module_Telegrambot_.Models;
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
using Telegram.Bot.Types.ReplyMarkups;

namespace HomeWork__new_module_Telegrambot_.Controller
{
    public class TextMessageController : MemoryStorage
    {
        private readonly ITelegramBotClient _telegramClient;
        private InlineKeyboardController _inlineKeyboard;
        private CallbackQuery callbackQuery;
        private Session _session;
        

        public TextMessageController(ITelegramBotClient telegramBotClient, InlineKeyboardController inlineKeyboard)
        {
            _telegramClient = telegramBotClient;
            _inlineKeyboard = inlineKeyboard;
            callbackQuery = new CallbackQuery();
            
            
        }
        public async Task Handle( Message message, CallbackQuery callbackQuery,  CancellationToken ct)
        {
            switch (message.Text)
            {
                
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Length message" , $"Информация"),
                        InlineKeyboardButton.WithCallbackData($"Sum" , $"Сумма")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот может умеет:</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}1.Можно посчитать колличество символов в предложении.{Environment.NewLine}2.Функция простого калькулятора.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    callbackQuery.Data = GetChoice();
                    switch (callbackQuery.Data)
                    {
                        case "Информация":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id,$"Колличество символов в вашем предложении: {message.Text.Length}");
                            return;
                        case "Сумма":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел равна: {GetSum(message)}");
                            return;
                    }
                    await _inlineKeyboard.Handle(callbackQuery, ct);
                    break;
            }
        }
        private int GetSum(Message e)
        {
            string tempmessage = e.Text;
            char separator = ' ';
            string[]tempArr = tempmessage.Split(separator);
            int result = 0;
            foreach (var item in tempArr)
            {
                int temp = Convert.ToInt32(item);
                result += temp;
            }
            return result;
        }
    }
}
    

