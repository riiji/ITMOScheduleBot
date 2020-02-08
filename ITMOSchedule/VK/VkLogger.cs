using System;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using Telegram.Bot;
using VkNet;
using VkNet.Model;
using MihaZupan;

namespace ITMOSchedule.Bot
{
    public class VKLogger : ILogger
    {
        private readonly VkApi _vkApi;

        public VKLogger(VkApi vkApi)
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