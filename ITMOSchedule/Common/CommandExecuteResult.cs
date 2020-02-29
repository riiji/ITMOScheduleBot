using System;

namespace ITMOSchedule.Common
{
    public class CommandExecuteResult
    {
        public CommandExecuteResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public CommandExecuteResult(bool isSuccess, string executeMessage)
        {
            IsSuccess = isSuccess;
            ExecuteMessage = executeMessage;
        }

        public string ExecuteMessage { get; } = string.Empty;
        public bool IsSuccess { get; }
    }
}