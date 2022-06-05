using HomeWork__new_module_Telegrambot_.Controller;
using HomeWork__new_module_Telegrambot_.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HomeWork__new_module_Telegrambot_.Services
{
    public class MemoryStorage :IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;
        private static Session newSession;
         public MemoryStorage() 
        {
            _sessions = new ConcurrentDictionary<long, Session>();
            
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
            {
                newSession.Choice = InlineKeyboardController.GetCallbackQuery().Data;
                return _sessions[chatId];
            }
                

            // Создаем и возвращаем новую, если такой не было
             newSession = new Session();
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
       
        public static string GetChoice()
        {
            return newSession.Choice;
        }
    }
}
