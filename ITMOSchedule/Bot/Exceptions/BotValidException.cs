using System;

namespace ITMOSchedule.Bot.Exceptions
{
    public class BotValidException : NullReferenceException 
    {
        public BotValidException()
        {

        }

        public BotValidException(string message) : base(message)
        {

        }
    }
}