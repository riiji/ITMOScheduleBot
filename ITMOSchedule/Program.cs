using System;
using System.Threading.Tasks;
using ItmoSchedule.BotCommands;
using ItmoSchedule.Database;
using MessengerBotFramework.BotFramework;
using MessengerBotFramework.BotFramework.CommandControllers;
using MessengerBotFramework.MessangerApiProviders.VK;
using MessengerBotFramework.Tools;

namespace ItmoSchedule
{
    internal static class Program
    {
        private static async Task MainAsync()
        { 
            var apiProvider = new VkBotApiProvider(ReadVkSettings());
            var authTaskResult = apiProvider.Initialize();
            LoggerHolder.Log.Verbose(authTaskResult.ExecuteMessage);

            var commandList = new CommandsList
            {
                new PingCommand(),
                new ScheduleCommand(),
                new SetGroupCommand()
            };

            var bot = new Bot(apiProvider, commandList);
            bot.Process();
            await Task.Delay(-1);
        }

        private static void Main() => MainAsync().GetAwaiter().GetResult();

        private static VkSettings ReadVkSettings()
        {
            using var db = new DatabaseContext();
            var settings = db.BotSettings;

            var key = settings.Find("VkKey").Value;
            var appId = Convert.ToInt32(settings.Find("VkAppId").Value);
            var appSecret = settings.Find("VkAppSecret").Value;
            var groupId = Convert.ToInt32(settings.Find("VkGroupId").Value);
            return new VkSettings(key, appId, appSecret, groupId);
        }
    }
}
