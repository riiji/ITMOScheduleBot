using ItmoSchedule.Common;

namespace ItmoSchedule.Abstractions
{
    public interface IBotCommand
    {
        string CommandName { get; }

        string Description { get; }

        string[] Args { get; }

        bool CanExecute(CommandArgumentContainer args);

        TaskExecuteResult Execute(CommandArgumentContainer args);
    }
}