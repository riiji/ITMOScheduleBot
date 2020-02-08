using System;
using System.Collections.Generic;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;

namespace ITMOSchedule.Bot
{
    public class Bot<T> : IBot<T>
    {
        private readonly ILogger _logger;
        private readonly IInput<T> _inputter;
        private readonly IHandler<T> _handler;
        private readonly IPrinter _printer;

        public Bot(ILogger logger, IInput<T> inputter, IHandler<T> handler, IPrinter printer)
        {
            _logger = logger;
            _inputter = inputter;
            _handler = handler;
            _printer = printer;
        }

        public bool IsValid
        {
            get
            {
                if(_printer == null)
                    throw new BotValidException("Printer not founded");

                if(_handler == null)
                    throw new BotValidException("Handler not founded");

                if(_inputter == null)
                    throw new BotValidException("Inputter not founded");

                return true;
            }
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
            if(!IsValid)
                throw new BotValidException();

            var rawData = GetData();
            var data = HandleData(rawData);
            PrintData(data);
        }

        public void Login()
        {
            _logger.Login();
        }
    }
}