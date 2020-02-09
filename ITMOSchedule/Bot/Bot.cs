using System;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;

namespace ITMOSchedule.Bot
{
    public class Bot<T> : IBot<T>
    {
        private readonly IAuthorize _authorizer;
        private readonly IInput<T> _inputter;
        private readonly IHandler<T> _handler;
        private readonly IPrinter _printer;

        public Bot(IAuthorize authorizer, IInput<T> inputter, IPrinter printer)
        {
            _authorizer = authorizer ?? throw new BotValidException("Authorizer not founded");
            _inputter = inputter ?? throw new BotValidException("Inputter not founded");
            _printer = printer ?? throw new BotValidException("Printer not founded");
        }

        public T GetData()
        {
            return _inputter.GetData();
        }

        public string HandleData(T data)
        {
            return _handler.HandleData(data);
        }

        public void PrintData(string data)
        {
            _printer.PrintData(data);
        }

        public void Process()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            _authorizer.Login();
        }
    }
}