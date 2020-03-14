using ItmoSchedule.Abstractions;
using ItmoSchedule.Common;

namespace ItmoSchedule.BotCommands
{
    public class PingCommand : IBotCommand
    {
        public string CommandName { get; } = "Ping";
        public string Description { get; } = "Answer pong on ping message";
        public string[] Args { get; } = new string[0];

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public TaskExecuteResult Execute(CommandArgumentContainer args)
        {
            return new TaskExecuteResult(true, "Pong");
        }
    }
}