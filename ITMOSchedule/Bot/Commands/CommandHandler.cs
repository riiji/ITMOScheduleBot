using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Bot.Commands.List;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.VK;
using ItmoScheduleApiWrapper;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandHandler
    {
        private readonly CommandsList _commands;
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public CommandHandler(CommandsList commands, IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _commands = commands;
            _botProvider = botProvider;
            _itmoProvider = itmoProvider;

            RegisterCommands();
        }

        public bool IsCommandCorrect(CommandArgumentContainer userCommand)
        {
            var command = _commands.GetCommand(userCommand.CommandName);

            return command.CanExecute(new CommandArgumentContainer(userCommand.Arguments, userCommand.CommandName));
        }

        public Task RegisterCommands()
        {
            //TODO: move out from this class
            _commands.AddCommand(new PingCommand(_botProvider));
            _commands.AddCommand(new GetGroupScheduleCommand(_botProvider, _itmoProvider));
            
            return Task.CompletedTask;
        }

        public Task ExecuteCommand(CommandArgumentContainer args)
        {
            var command = _commands.GetCommand(args.CommandName);

            command.Execute(args);

            return Task.CompletedTask;
        }
    }
}