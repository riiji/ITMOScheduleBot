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
            var bot = new Bot(apiProvider);
            bot.Process();
            await Task.Delay(-1);
        }

        private static void Main() => MainAsync().GetAwaiter().GetResult();
    }
}
