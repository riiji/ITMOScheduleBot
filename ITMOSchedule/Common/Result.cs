using System;

namespace ItmoSchedule.Common
{
    public class Result<T> : TaskExecuteResult
    {
        public Result(bool isSuccess, T result) : base(isSuccess)
        {
            Value = result;
        }

        public Result(bool isSuccess, string executeMessage, T result) : base(isSuccess, executeMessage)
        {
            Value = result;
        }

        public readonly T Value;
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
        public string ExecuteMessage { get; } = string.Empty;
        public bool IsSuccess { get; }
    }
}