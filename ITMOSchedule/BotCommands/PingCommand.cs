using MessengerBotFramework.Abstractions;
using MessengerBotFramework.Common;

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

        public Result Execute(CommandArgumentContainer args)
        {
            return new Result(true, "Pong");
        }
    }
}