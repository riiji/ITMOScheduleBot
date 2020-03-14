using System;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Common;

namespace ItmoSchedule.Abstractions
{
    public interface IBotApiProvider
    {
        public event EventHandler<BotEventArgs> OnMessage;

        public TaskExecuteResult Initialize();
    }
}