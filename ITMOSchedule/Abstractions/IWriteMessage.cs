using ItmoSchedule.Common;

namespace ItmoSchedule.Abstractions
{
    public interface IWriteMessage
    {
        public TaskExecuteResult WriteMessage(SenderData sender, string message);
    }
}