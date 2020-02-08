namespace ITMOSchedule.Bot.Interfaces
{
    public interface IInput<out TI>
    {
        TI GetData();
    }
}