using System;
using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot
{
    public static class Utilities
    {
        private static readonly Random Random = new Random();
        public static int GetRandom()
        {
            return Random.Next();
        }

        public static Task<CommandContainer> ParseCommand(string commandsWithArgs)
        {
            var command = commandsWithArgs.Split().FirstOrDefault();

            if (command == null)
                return Task.FromException<CommandContainer>(new BotValidException("Parse error, command not founded"));

            // args without command
            var args = commandsWithArgs.Split().Skip(1).ToArray();

            return Task.FromResult(new CommandContainer(command, args));
        }
    }

}