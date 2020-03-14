using System;

namespace ItmoSchedule.Common
{
    public class TaskExecuteResult<T> : TaskExecuteResult
    {
        public TaskExecuteResult(bool isSuccess, T result) : base(isSuccess)
        {
            Result = result;
        }

        public TaskExecuteResult(bool isSuccess, string executeMessage, T result) : base(isSuccess, executeMessage)
        {
            Result = result;
        }

        public readonly T Result;
    }

    public class TaskExecuteResult
    {
        public TaskExecuteResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public TaskExecuteResult(bool isSuccess, string executeMessage)
        {
            IsSuccess = isSuccess;
            ExecuteMessage = executeMessage;
        }

        public TaskExecuteResult WithException<T>(T exception) where T : Exception
        {
            _exception = exception;
            return this;
        }

        public Exception GetException()
        {
            return _exception;
        }

        private Exception _exception;
        public string ExecuteMessage { get; private set; } = string.Empty;
        public bool IsSuccess { get; }
    }
}