namespace ITMOSchedule.Bot.Interfaces
{
    public interface IBot<T> : IAuthorize, IInput<T>, IHandler<T>, IPrinter
    {
        void Process();
    }
}