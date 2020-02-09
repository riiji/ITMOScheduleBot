using System;

namespace ITMOSchedule.Common
{
    public class CommandExecuteResult
    {
        public CommandExecuteResult(Boolean isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public bool isSuccess { get; }
    }
}