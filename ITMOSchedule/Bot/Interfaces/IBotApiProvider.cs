using System;
using System.Threading.Tasks;

namespace ITMOSchedule.Bot.Interfaces
{
    public interface IBotApiProvider
    {
        public event EventHandler<BotEventArgs> OnMessage;

        public Task WriteMessage(int groupId, string message);
    }
}