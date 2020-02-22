using System;
using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Common;

namespace ItmoSchedule.BotFramework
{
    public static class Utilities
    {
        private static readonly Random Random = new Random();

        public const int GroupNameLength = 5;

        public static int GetRandom()
        {
            return Random.Next();
        }

        public static Task<CommandArgumentContainer> ParseCommand(string commandsWithArgs, int groupId)
        {
            var commands = commandsWithArgs.Split();

            var commandName = commands.FirstOrDefault();

            // skip command name
            var args = commands.Skip(1);

            return Task.FromResult(new CommandArgumentContainer(groupId, args.ToList(), commandName));
        }

        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        public static void Log(LogLevel level, string message)
        {
            Console.WriteLine($"{DateTime.Now} | {level} : {message}");
        }

    }

}