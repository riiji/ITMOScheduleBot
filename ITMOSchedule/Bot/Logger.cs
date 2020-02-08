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
            var proxyF = new HttpToSocks5Proxy("orbtl.s5.opennetwork.cc", 999, "409428213", "OqU8K9W4");

            var token = "935172409:AAF9sZuWrEo_AgePRVD-WC-lzNYa8fxdAbw";
            var botClient = new TelegramBotClient(token, proxyF);
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}