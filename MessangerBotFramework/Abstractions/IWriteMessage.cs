using MessengerBotFramework.Common;

namespace MessengerBotFramework.Abstractions
{
    public interface IWriteMessage
    {
        Result WriteMessage(SenderData sender, string message);
    }
}