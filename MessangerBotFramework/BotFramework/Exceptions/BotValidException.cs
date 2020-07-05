using System;

namespace MessengerBotFramework.BotFramework.Exceptions
{
    public class BotValidException : ArgumentNullException 
    {
        public BotValidException()
        {

        }

        public BotValidException(string message) : base(message)
        {

        }
    }
}