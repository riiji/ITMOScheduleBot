using System.Collections.Generic;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;

namespace ITMOSchedule.Bot.Commands
{
    public static class CommandsList
    {
        private static readonly Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

        public static void AddCommand(ICommand command)
        {
            Commands.Add(command.CommandName, command);
        }

        public static ICommand GetCommand(string commandName)
        {
            Commands.TryGetValue(commandName, out ICommand command);

            if(command == null)
                throw new BotValidException("Command not founded!");

            return command;
        }
    }
}