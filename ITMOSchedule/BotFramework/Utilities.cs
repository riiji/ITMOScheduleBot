﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public static CommandArgumentContainer ParseCommand(string commandsWithArgs, int groupId)
        {
            string[] commands = commandsWithArgs.Split();

            var commandName = commands.FirstOrDefault();

            // skip command name
            IEnumerable<string> args = commands.Skip(1);

            return new CommandArgumentContainer(commandName, new SenderData(groupId), args.ToList());
        }
    }

}