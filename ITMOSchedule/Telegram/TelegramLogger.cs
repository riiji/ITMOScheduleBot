﻿using System;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Telegram;
using Telegram.Bot;

namespace ITMOSchedule.Bot
{
    public class TelegramLogger : ILogger
    {
        private readonly TelegramBotClient _telegramApi;

        public TelegramLogger(TelegramBotClient telegramApi)
        {
            _telegramApi = telegramApi;
        }

        public void Login()
        {
            var botClient = new TelegramBotClient(TelegramSettings.Key, TelegramSettings.Proxy);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}