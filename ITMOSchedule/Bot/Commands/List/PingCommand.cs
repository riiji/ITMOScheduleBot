using System;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands.List
{
    public class PingCommand : IBotCommand
    {
        public string CommandName { get; } = "Ping";
        public string Description { get; } = "Answer pong on ping message";
        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            // TODO: Realize
            Console.WriteLine("Pong");

            return new CommandExecuteResult(true);
        }
    }
}