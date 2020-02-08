namespace ITMOSchedule.Bot.Interfaces
{
    public interface IHandler<in TI, out TO>
    {
        public TO HandleData(TI data);
    }
}