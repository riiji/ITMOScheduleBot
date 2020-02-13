using System.Threading.Tasks;

namespace ITMOSchedule.Bot.Interfaces
{
    public interface IBotApiProvider
    {
        public delegate void MessageDelegate(object sender, BotEventArgs e);

        public event MessageDelegate OnMessage;

        public Task WriteMessage(int groupId, string message);
    }
}