using System;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands.List
{
    public class PingCommand : IBotCommand
    {
        private IBotApiProvider _provider;

        public PingCommand(IBotApiProvider provider)
        {
            _provider = provider;
        }

        public string CommandName { get; } = "Ping";

        public string Description { get; } = "Answer pong on ping message";

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            _provider.WriteMessage(args.GroupId, "Pong");

            return new CommandExecuteResult(true);
        }
    }
}