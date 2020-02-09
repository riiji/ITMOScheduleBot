using System.Collections.Generic;
using System.Linq;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandsList
    {
        private readonly Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

        public void AddCommand(ICommand command)
        {
            Commands.Add(command.CommandName, command);
        }

        public ICommand GetCommand(string commandName)
        {
            Commands.TryGetValue(commandName, out ICommand command);

            if(command == null)
                throw new BotValidException("Command not founded!");

            return command;
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