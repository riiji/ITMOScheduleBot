using System.Threading.Tasks;
using ITMOSchedule.VK;

namespace ITMOSchedule
{
    class Program
    {
        public async Task MainAsync()
        {
            var bot = new Bot.Bot(new VkBotApiProvider(new VkAuthorizer()));

            bot.Process();

            await Task.Delay(-1);
        }

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
    }
}
