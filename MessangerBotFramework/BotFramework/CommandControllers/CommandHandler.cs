using MessengerBotFramework.Common;

namespace MessengerBotFramework.BotFramework.CommandControllers
{
    public class CommandHandler
    {
        private readonly CommandsList _commands;

        public CommandHandler(CommandsList commands)
        {
            _commands = commands;
        }

        public Result IsCommandCorrect(CommandArgumentContainer args)
        {
            var commandTask = _commands.GetCommand(args.CommandName);
            if (!commandTask.IsSuccess)
                return commandTask;

            var command = commandTask.Value;

            return command.CanExecute(args)
                ? new Result(true, $"command {command.CommandName} can be executable with args {string.Join(' ', args.Arguments)}")
                : new Result(false, $"command {command.CommandName} not executable with args {string.Join(' ', args.Arguments)}");
        }

        public Result ExecuteCommand(CommandArgumentContainer args)
        {
            return _commands
                .GetCommand(args.CommandName)
                .ContinueOnSuccess(command => command.Execute(args));
        }
    }
}