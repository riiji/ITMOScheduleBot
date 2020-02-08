namespace ITMOSchedule.Bot.Interfaces
{
    public interface IBot<T> : ILogger, IInput<T>, IValidator, IHandler<T>, IPrinter
    {
        void Process();
    }
}