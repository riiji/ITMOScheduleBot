namespace ITMOSchedule.Bot.Interfaces
{
    public interface IBot<TI, TO> : ILogger, IInput<TI>, IValidator, IHandler<TI,TO>, IPrinter<TO>
    {
        TI InputData => GetData();

        TO OutputData => HandleData(InputData);

        void Process();
    }
}