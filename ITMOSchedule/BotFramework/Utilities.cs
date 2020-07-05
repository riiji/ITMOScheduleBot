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

        public static CommandArgumentContainer ParseCommand(BotEventArgs botArgs)
        {
            string[] commands = botArgs.Text.Split();

            var commandName = commands.FirstOrDefault() ?? throw new BotValidException("ParseCommand: commandName was null");

            //commandName = prefixCommand + commandName.ToLower();

            //skip command name
            IEnumerable<string> args = commands.Skip(1);

            return new CommandArgumentContainer(commandName, new SenderData(botArgs.GroupId), args.ToList());
        }
    }

}