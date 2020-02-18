using ITMOSchedule.Common;

namespace ItmoSchedule.BotFramework.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }

        string Description { get; }

        bool CanExecute(CommandArgumentContainer args);

        CommandExecuteResult Execute(CommandArgumentContainer args);
    }
}