using System.Threading.Tasks;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Tools.Loggers;
using ItmoSchedule.VK;

namespace ItmoSchedule
{
    internal static class Program
    {
        private static async Task MainAsync()
        { 
            var apiProvider = new VkBotApiProvider(new VkSettings());
            var authTaskResult = apiProvider.Initialize();
            Logger.Info(authTaskResult.ExecuteMessage);
            var api = apiProvider.GetApi();
            var bot = new Bot(apiProvider, new VkWriteMessage(api.Messages));
            bot.Process();
            await Task.Delay(-1);
        }

        private static void Main() => MainAsync().GetAwaiter().GetResult();
    }
}
