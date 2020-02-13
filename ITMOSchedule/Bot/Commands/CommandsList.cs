using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Bot.Commands.List;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandsList
    {
        private readonly Dictionary<string, IBotCommand> _commands = new Dictionary<string, IBotCommand>();

        public Task AddCommand(IBotCommand command)
        {
            if (_commands.TryAdd(command.CommandName, command))
                return Task.CompletedTask;
            return Task.FromException(new BotValidException($"Command {command.CommandName} cant be added"));
        }

        public Task<IBotCommand> GetCommand(string commandName)
        {
            _commands.TryGetValue(commandName, out IBotCommand command);

            if (command == null)
                return Task.FromException<IBotCommand>(new BotValidException("Command not founded!"));

            return Task.FromResult(command);
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