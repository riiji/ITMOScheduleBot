namespace ITMOSchedule.Bot.Interfaces
{
    public interface IPrinter<in TO>
    {
        void PrintData(TO data);
    }
}