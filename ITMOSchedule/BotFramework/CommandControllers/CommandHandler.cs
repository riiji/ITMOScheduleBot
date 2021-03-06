﻿using System.Linq;
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

        public Result IsCommandCorrect(CommandArgumentContainer args)
        {
            var commandTask = _commands.GetCommand(args.CommandName);

            if (!commandTask.IsSuccess)
                return commandTask;

            var command = commandTask.Value;

            if (command.CanExecute(args))
                return new Result(true, $"command {args.CommandName} can be executable with args {string.Join(' ', args.Arguments.Select(x => x))}");

            var loggerMessage = $"command {command.CommandName} not executable with args {string.Join(' ', args.Arguments.Select(x=>x))}";
            //loggerMessage = args.Arguments.Aggregate(loggerMessage, (current, t) => current + (t + " "));

            return new Result(false, loggerMessage);
        }

        public void RegisterCommand(IBotCommand command)
        {
            _commands.AddCommand(command);
        }

        public Result ExecuteCommand(CommandArgumentContainer args)
        {
            Result<IBotCommand> commandTask = _commands.GetCommand(args.CommandName);

            if (!commandTask.IsSuccess)
                return commandTask;

            var command = commandTask.Value;
            var commandExecuteResult = command.Execute(args);

            return commandExecuteResult;
        }

        public CommandsList GetCommands()
        {
            return _commands;
        }
    }
}