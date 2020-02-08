using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Interfaces;
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
        static void Main(string[] args)
        {
            var vkApi = new VkApi();

            var logger = new Logger(vkApi);

            logger.Login();

            var inputter = new Inputter(vkApi);
            var handler = new Handler(vkApi);
            var printer = new Printer();

            var bot = new Bot<LongPollHistoryResponse, string, string>(printer, handler, inputter, logger);

            bot.Process();
        }
    }
}
