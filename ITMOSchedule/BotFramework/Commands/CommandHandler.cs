using System.Linq;
using System.Threading.Tasks;
using ItmoSchedule.BotFramework.Commands.List;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoScheduleApiWrapper;

namespace ItmoSchedule.BotFramework.Commands
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

            return command.CanExecute(new CommandArgumentContainer(0, userCommand.Arguments, userCommand.CommandName));
        }

        public Task RegisterCommands()
        {
            //TODO: move out from this class
            _commands.AddCommand(new PingCommand(_botProvider));
            _commands.AddCommand(new GetGroupScheduleCommand(_botProvider, _itmoProvider));
            _commands.AddCommand(new HelpCommand(_botProvider, _commands));

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