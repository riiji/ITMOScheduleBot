using ItmoSchedule.Common;

namespace ItmoSchedule.Abstractions
{
    public interface IWriteMessage
    {
        public Result WriteMessage(SenderData sender, string message);
    }
}