using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.VK;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Filters;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;
using VkNet;
using VkNet.Model;
using VkNet.Utils;

namespace ITMOSchedule
{
    class Program
    {
        public async Task MainAsync()
        {
            VkBot bot = new VkBot();
            bot.Login();

            await Task.Delay(-1);
        }

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
    }
}
