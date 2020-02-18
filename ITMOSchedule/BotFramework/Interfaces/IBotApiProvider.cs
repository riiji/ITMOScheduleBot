using System;

namespace ItmoSchedule.BotFramework.Interfaces
{
    public interface IBotApiProvider
    {
        public event EventHandler<BotEventArgs> OnMessage;

        public void WriteMessage(int groupId, string message);
    }
}