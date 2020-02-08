using System;
using ITMOSchedule.Bot.Interfaces;
using Telegram.Bot;
using MihaZupan;

namespace ITMOSchedule.Bot
{
    public class TelegramLogger : ILogger
    {
        private readonly TelegramBotClient _TelegramApi;

        public TelegramLogger(TelegramBotClient _TelegramApi)
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
