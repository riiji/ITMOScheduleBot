using System.Threading.Tasks;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Tools.Loggers;
using ItmoSchedule.VK;
using ITMOSchedule.VK;

namespace ItmoSchedule
{
    internal class Program
    {
        public async Task MainAsync()
        { 
            var apiProvider = new VkBotApiProvider(new VkSettings());
            var authTaskResult = apiProvider.Initialize();
            Logger.Info(authTaskResult.ExecuteMessage);
            var api = apiProvider.GetApi();
            var bot = new Bot(apiProvider, new VkWriteMessage(api.Messages));
            bot.Process();
            await Task.Delay(-1);
        }

        private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
    }
}
