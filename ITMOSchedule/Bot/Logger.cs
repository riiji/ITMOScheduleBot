using System;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using Telegram.Bot;
using VkNet;
using VkNet.Model;
using MihaZupan;

namespace ITMOSchedule.Bot
{
    public class VkLogger : ILogger
    {
        private readonly VkApi _vkApi;

        public VkLogger(VkApi vkApi)
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

    public class TelegramLogger : ILogger
    {
        public void Login()
        {

        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}