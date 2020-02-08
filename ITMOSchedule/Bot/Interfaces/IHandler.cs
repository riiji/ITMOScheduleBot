namespace ITMOSchedule.Bot.Interfaces
{
    public interface IHandler<in TI>
    {
        public string HandleData(TI data);
    }
}