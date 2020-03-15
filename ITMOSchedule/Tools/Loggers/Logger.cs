using System;

namespace ItmoSchedule.Tools.Loggers
{
    public static class Logger
    {
        static Logger()
        {
            Log = new ConsoleLogger();

            Message($"[SYS] Start new session {DateTime.Now:G}");
        }

        private static readonly ILogger Log;
        private static readonly LogLevel LogLevel = LogLevel.Info;

        public static void Info(string message) => InternalWrite(message, LogLevel.Info);
        public static void Message(string message) => InternalWrite(message, LogLevel.Message);
        public static void Warning(string message) => InternalWrite(message, LogLevel.Warning);
        public static void Error(string message) => InternalWrite(message, LogLevel.Error);

        private static void InternalWrite(string message, LogLevel logLevel)
        {
            if (LogLevel <= logLevel)
                Log.Write($"[{logLevel}] {message}");
        }
    }
}