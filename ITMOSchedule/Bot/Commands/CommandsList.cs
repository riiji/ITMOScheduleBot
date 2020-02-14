using System;
using System.Collections.Generic;
using System.Linq;
using ITMOSchedule.Bot.Commands.List;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandsList
    {
        private readonly Dictionary<string, IBotCommand> _commands = new Dictionary<string, IBotCommand>();

        public void AddCommand(IBotCommand command)
        {
            _commands.Add(command.CommandName, command);
        }

        public IBotCommand GetCommand(string commandName)
        {
            if(_commands.TryGetValue(commandName, out IBotCommand command))
                return command;

            throw new BotValidException("Command not founded!");
        }

        public bool IsCommandExisted(string commandName)
        {
            return _commands.ContainsKey(commandName);
        }

        public List<CommandExecuteResult> ExecuteAllAvailable(CommandArgumentContainer args)
        {
            return _commands.Values
                .Where(command => command.CanExecute(args))
                .Select(command => command.Execute(args))
                .ToList();
        }

        public CommandExecuteResult TryExecuteFirstAvailable(CommandArgumentContainer args)
        {
            return _commands.Values
                .Where(command => command.CanExecute(args))
                .Select(command => command.Execute(args))
                .FirstOrDefault();
        }
    }
}