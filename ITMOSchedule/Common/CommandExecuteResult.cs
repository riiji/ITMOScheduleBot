namespace ITMOSchedule.Common
{
    public class CommandExecuteResult
    {
        public CommandExecuteResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}