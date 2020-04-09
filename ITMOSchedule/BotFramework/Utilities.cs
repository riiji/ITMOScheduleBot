using System;
using System.Collections.Generic;
using System.Linq;
using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.Common;

namespace ItmoSchedule.BotFramework
{
    public static class Utilities
    {
        private static readonly Random Random = new Random();

        public static int GetRandom()
        {
            return Random.Next();
        }

        public static CommandArgumentContainer ParseCommand(string commandsWithArgs, int groupId, char prefixCommand)
        {
            string[] commands = commandsWithArgs.Split();

            var commandName = commands.FirstOrDefault();

            if (commandName == null)
                throw new BotValidException("ParseCommand: commandName was null");

            //commandName = prefixCommand + commandName.ToLower();

            //skip command name
            IEnumerable<string> args = commands.Skip(1);

            return new CommandArgumentContainer(commandName, new SenderData(groupId), args.ToList());
        }
    }

}