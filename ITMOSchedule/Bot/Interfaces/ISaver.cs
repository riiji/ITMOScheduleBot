namespace ITMOSchedule.Bot.Interfaces
{
    public interface ISaver<T>
    {
        void SaveData(T data);
    }
}