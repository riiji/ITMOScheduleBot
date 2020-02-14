using System;

namespace ITMOSchedule.Common
{
    public class CommandExecuteResult
    {
        public CommandExecuteResult(Boolean isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}