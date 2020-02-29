using System;

namespace ItmoSchedule.Tools.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}