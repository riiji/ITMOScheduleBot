using System.Collections.Generic;
using System.Linq;
using ITMOSchedule.Bot.Exceptions;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandsList
    {
        private static readonly Dictionary<string, IBotCommand> Commands = new Dictionary<string, IBotCommand>();

        public static void AddCommand(IBotCommand command)
        {
            Commands.Add(command.CommandName, command);
        }

        public static IBotCommand GetCommand(string commandName)
        {
            Commands.TryGetValue(commandName, out IBotCommand command);

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