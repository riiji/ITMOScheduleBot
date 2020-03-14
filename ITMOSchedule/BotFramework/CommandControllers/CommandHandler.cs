using System.Linq;
using ItmoSchedule.Abstractions;
using ItmoSchedule.Common;

namespace ItmoSchedule.BotFramework.CommandControllers
{
    public class CommandHandler
    {
        private readonly CommandsList _commands;

        public CommandHandler(CommandsList commands)
        {
            _commands = commands;
        }

        public TaskExecuteResult IsCommandCorrect(CommandArgumentContainer args)
        {
            TaskExecuteResult<IBotCommand> commandTask = _commands.GetCommand(args.CommandName);

            if(!commandTask.IsSuccess)
                return commandTask;

            var command = commandTask.Result;

            if(command.CanExecute(args))
                return new TaskExecuteResult(true, "ok");

            var loggerMessage = $"command {command.CommandName} not executable with args ";
            loggerMessage = args.Arguments.Aggregate(loggerMessage, (current, t) => current + (t + " "));
 
            return new TaskExecuteResult(false, loggerMessage);
        }

        public void RegisterCommand(IBotCommand command)
        {
            _commands.AddCommand(command);
        }

        public TaskExecuteResult ExecuteCommand(CommandArgumentContainer args)
        {
            TaskExecuteResult<IBotCommand> commandTask = _commands.GetCommand(args.CommandName);

            if (!commandTask.IsSuccess)
                return commandTask;

            var command = commandTask.Result;
            var commandExecuteResult = command.Execute(args);
            
            return commandExecuteResult;
        }

        public CommandsList GetCommands()
        {
            return _commands;
        }
    }
}