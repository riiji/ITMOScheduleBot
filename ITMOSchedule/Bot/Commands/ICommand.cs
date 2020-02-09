using ITMOSchedule.Common;

namespace ITMOSchedule.Commands
{
    //TODO: rename coz of collision with System.Input.ICommand
    public interface ICommand
    {
        string CommandName { get; }

        string Description { get; }

        bool CanExecute(CommandArgumentContainer args);
        CommandExecuteResult Execute(CommandArgumentContainer args);
    }
}