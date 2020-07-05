using System;
using MessengerBotFramework.BotFramework;
using MessengerBotFramework.Common;

namespace MessengerBotFramework.Abstractions
{
    public interface IBotApiProvider : IWriteMessage
    {
        event EventHandler<BotEventArgs> OnMessage;
        void Dispose();
        Result Initialize();
    }
}