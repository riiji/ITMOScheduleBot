using System;
using System.Collections.Generic;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;

namespace ITMOSchedule.Bot
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TI">Bot Input Type</typeparam>
    /// <typeparam name="TO">Bot Output Type</typeparam>
    /// <typeparam name="TS">Bot Saver Type</typeparam>
    public class Bot<TI, TO, TS> : IBot<TI, TO>, ISaver<TS>
    {
        private readonly ILogger _logger;
        private readonly IInput<TI> _inputter;
        private readonly IHandler<TI, TO> _handler;
        private readonly IPrinter<TO> _printer;
        private readonly ISaver<TS> _saver;

        public Bot(IPrinter<TO> printer, IHandler<TI, TO> handler, IInput<TI> inputter, ILogger logger, ISaver<TS> saver)
        {
            _printer = printer;
            _handler = handler;
            _inputter = inputter;
            _logger = logger;
            _saver = saver;
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

        public TI GetData()
        {
            return _inputter.GetData();
        }

        public TO HandleData(TI data)
        {
            return _handler.HandleData(data);
        }

        public void PrintData(TO data)
        {
            _printer.PrintData(data);
        }

        public void SaveData(TS data)
        {
            throw new NotImplementedException();
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

        public void Logout()
        {
            _logger.Logout();
        }
    }
}