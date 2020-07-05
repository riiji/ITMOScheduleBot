using System;
using MessengerBotFramework.BotFramework;
using MessengerBotFramework.Common;

namespace MessengerBotFramework.Abstractions
{
    public interface IBotApiProvider : IWriteMessage, IDisposable
    {
        event EventHandler<BotEventArgs> OnMessage;
        Result Initialize();
    }
}