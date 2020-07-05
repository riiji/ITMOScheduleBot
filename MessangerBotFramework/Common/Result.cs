using System;

namespace MessengerBotFramework.Common
{
    public class Result<T> : Result
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

    public class Result
    {
        public Result(bool isSuccess) : this(isSuccess, null)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string executeMessage)
        {
            IsSuccess = isSuccess;
            _executeMessage = executeMessage;
        }

        public Result WithException<T>(T exception) where T : Exception
        {
            Exception = exception;
            return this;
        }

        private readonly string _executeMessage;

        public Exception Exception { get; set; }
        public bool IsSuccess { get; }

        public string ExecuteMessage
        {
            get
            {
                if (Exception != null) return $"{_executeMessage}" + Environment.NewLine + Exception.Message;
                return _executeMessage;
            }
        }
    }
}