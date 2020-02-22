using System.Threading.Tasks;
using ItmoSchedule.BotFramework.Commands.List;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.Extensions;
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
            var commandTask = _commands.GetCommand(userCommand.CommandName);
            commandTask.WaitSafe();

            if (commandTask.IsFaulted)
                return false;

            IBotCommand command = commandTask.Result;

            return command.CanExecute(new CommandArgumentContainer(0, userCommand.Arguments, userCommand.CommandName));
        }

        public void RegisterCommands()
        {
            _commands.AddCommand(new PingCommand(_botProvider));
            _commands.AddCommand(new GetGroupScheduleCommand(_botProvider, _itmoProvider));
            _commands.AddCommand(new GetTodayGroupScheduleCommand(_botProvider, _itmoProvider));
            _commands.AddCommand(new HelpCommand(_botProvider, _commands));
        }

        public Task<CommandExecuteResult> ExecuteCommand(CommandArgumentContainer args)
        {
            var commandTask = _commands.GetCommand(args.CommandName);
            commandTask.WaitSafe();

            if (commandTask.IsFaulted)
                return Task.FromException<CommandExecuteResult>(commandTask.Exception);

            IBotCommand command = commandTask.Result;
            CommandExecuteResult commandExecuteResult = command.Execute(args);

            return Task.FromResult(commandExecuteResult);
        }
    }
}