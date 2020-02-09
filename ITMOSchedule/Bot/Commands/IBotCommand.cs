namespace ITMOSchedule.Bot.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }

        string Description { get; }

        void Execute(string[] args);
    }
}