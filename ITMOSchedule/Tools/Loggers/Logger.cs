using System;
using ItmoSchedule.Tools.Loggers;

namespace ItmoSchedule.Tools
{
    public class Logger
    {
        static Logger()
        {
            bool isFile = false;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (isFile)
                // ReSharper disable once HeuristicUnreachableCode
                Log = new FileLogger("debug-file-log.log");
            else
                Log = new DebugLogger();

            Message($"[SYS] Start new session {DateTime.Now:G}");
        }

        public static readonly ILogger Log;
        public static readonly LogLevel LogLevel = LogLevel.Message;

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