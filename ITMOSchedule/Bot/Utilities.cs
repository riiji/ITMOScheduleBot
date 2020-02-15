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

        public static Task<CommandArgumentContainer> ParseCommand(string commandsWithArgs)
        {
            //TODO: don't split twice 
            var commandName = commandsWithArgs.Split().FirstOrDefault();

            if (commandName == null)
                return Task.FromException<CommandArgumentContainer>(new BotValidException("Parse error, command not founded"));

            // args without command
            var args = commandsWithArgs.Split().Skip(1);

            return Task.FromResult(new CommandArgumentContainer(args.ToList(), commandName));
        }
    }

}