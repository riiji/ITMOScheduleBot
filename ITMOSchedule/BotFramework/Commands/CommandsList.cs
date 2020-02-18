using System.Collections.Generic;
using System.Linq;
using ItmoSchedule.BotFramework.Exceptions;
using ITMOSchedule.Common;

namespace ItmoSchedule.BotFramework.Commands
{
    public class CommandsList
    {
        public readonly Dictionary<string, IBotCommand> Commands = new Dictionary<string, IBotCommand>();

        public void AddCommand(IBotCommand command)
        {
            Commands.Add(command.CommandName, command);
        }

        public IBotCommand GetCommand(string commandName)
        {
            if(Commands.TryGetValue(commandName, out IBotCommand command))
                return command;

            throw new BotValidException("Command not founded!");
        }

        public bool IsCommandExisted(string commandName)
        {
            return Commands.ContainsKey(commandName);
        }

        public List<CommandExecuteResult> ExecuteAllAvailable(CommandArgumentContainer args)
        {
            return Commands.Values
                .Where(command => command.CanExecute(args))
                .Select(command => command.Execute(args))
                .ToList();
        }

        public CommandExecuteResult TryExecuteFirstAvailable(CommandArgumentContainer args)
        {
            return Commands.Values
                .Where(command => command.CanExecute(args))
                .Select(command => command.Execute(args))
                .FirstOrDefault();
        }
    }
}