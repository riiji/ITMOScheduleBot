using Serilog;
using Serilog.Core;

namespace MessengerBotFramework.Tools
{
    public static class LoggerHolder
    {
        public static Logger Log;

        static LoggerHolder()
        {
            Log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("bot-framework-log.txt")
                .CreateLogger();

            Log.Information("[SYS] Start new session");
        }
    }
}