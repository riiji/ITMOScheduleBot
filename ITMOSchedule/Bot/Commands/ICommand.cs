using VkNet.Model.RequestParams.Groups;

namespace ITMOSchedule.Commands
{
    public interface ICommand
    {
        string CommandName { get; }

        string Description { get; }

        void Execute(string[] args);
    }
}