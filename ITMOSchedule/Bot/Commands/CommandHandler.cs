using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Bot.Commands.List;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.VK;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandHandler
    {
        private readonly CommandsList _commands;
        private readonly IBotApiProvider _provider;

        public CommandHandler(CommandsList commands, IBotApiProvider provider)
        {
            _commands = commands;
            _provider = provider;

            RegisterCommands();
        }

        public bool IsCommandCorrect(CommandContainer userCommand)
        {
            var commandTask = _commands.GetCommand(userCommand.Name);

            if (commandTask.IsFaulted)
                return false;

            var command = commandTask.Result;

            return command.CanExecute(new CommandArgumentContainer(userCommand.Args.ToList()));
        }

        public bool IsCommandCorrect(string commandName, CommandArgumentContainer args)
        {
            return IsCommandCorrect(new CommandContainer(commandName, args.Arguments.ToArray()));
        }

        public bool IsCommandCorrect(string commandName, string[] args)
        {
            return IsCommandCorrect(commandName, new CommandArgumentContainer(args.ToList()));
        }

        public Task RegisterCommands()
        {
            _commands.AddCommand(new PingCommand(_provider));
            
            return Task.CompletedTask;
        }

        public Task ExecuteCommand(string commandName, CommandArgumentContainer args)
        {
            var commandTask = _commands.GetCommand(commandName);

            if (commandTask.IsFaulted)
                return Task.FromException(new BotValidException($"{commandTask.Exception.Message}"));

            var command = commandTask.Result;

            command.Execute(args);

            return Task.CompletedTask;
        }
    }
}