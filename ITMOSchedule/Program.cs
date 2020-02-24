using System.Threading.Tasks;
using ItmoSchedule.BotFramework;
using ItmoSchedule.VK;
using ITMOSchedule.VK;

namespace ItmoSchedule
{
    internal class Program
    {
        public async Task MainAsync()
        { 
            var apiProvider = new VkBotApiProvider(new VkSettings());
            apiProvider.Auth();

            var bot = new Bot(apiProvider);
            bot.Process();

            await Task.Delay(-1);
        }

        private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
    }
}
