using System.Threading.Tasks;
using ItmoSchedule.BotFramework.Commands.BotCommands;
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

        public CommandHandler(CommandsList commands, IBotApiProvider botProvider)
        {
            _commands = commands;
            _botProvider = botProvider;
            _itmoProvider = new ItmoApiProvider();

            RegisterCommands();
        }

        public bool IsCommandCorrect(CommandArgumentContainer userCommand)
        {
            //TODO: change var settings
            var commandTask = _commands.GetCommand(userCommand.CommandName);
            commandTask.WaitSafe();

            if (commandTask.IsFaulted)
                return false;

            IBotCommand command = commandTask.Result;

            //TODO: log that command wasn't execute
            return command.CanExecute(userCommand);
        }

        public void RegisterCommands()
        {
            _commands.AddCommand(new PingCommand(_botProvider));
            _commands.AddCommand(new HelpCommand(_botProvider, _commands));
            _commands.AddCommand(new SetGroupCommand(_botProvider));
            _commands.AddCommand(new ScheduleCommand(_botProvider, _itmoProvider));
        }

        public Task<CommandExecuteResult> ExecuteCommand(CommandArgumentContainer args)
        {
            var commandTask = _commands.GetCommand(args.CommandName);
            commandTask.WaitSafe();

            if (commandTask.IsFaulted)
                return Task.FromException<CommandExecuteResult>(commandTask.Exception);

            IBotCommand command = commandTask.Result;
            CommandExecuteResult commandExecuteResult = command.Execute(args);
            _botProvider.WriteMessage(args.Sender.GroupId, commandExecuteResult.ExecuteMessage);

            return Task.FromResult(commandExecuteResult);
        }
    }
}