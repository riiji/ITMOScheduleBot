using System;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Common;

namespace ItmoSchedule.Abstractions
{
    public interface IBotApiProvider : IWriteMessage
    {
        public event EventHandler<BotEventArgs> OnMessage;

        public Result Initialize();
    }
}