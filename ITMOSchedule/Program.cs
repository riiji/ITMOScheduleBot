﻿using System.Threading.Tasks;
using ItmoSchedule.BotFramework;
using ITMOSchedule.VK;
using VkApi.Wrapper.Services;

namespace ItmoSchedule
{
    internal class Program
    {
        public async Task MainAsync()
        { 
            var apiProvider = new VkBotApiProvider();
            apiProvider.Auth();

            var bot = new Bot(apiProvider);
            bot.Process();

            await Task.Delay(-1);
        }

        private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
    }
}
