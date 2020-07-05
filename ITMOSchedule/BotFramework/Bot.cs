using System;
using ItmoSchedule.Abstractions;
using ItmoSchedule.BotCommands;
using ItmoSchedule.BotFramework.CommandControllers;
using ItmoSchedule.Common;
using ItmoSchedule.Tools.Loggers;

namespace ItmoSchedule.BotFramework
{
    public class Bot : IDisposable
    {
        private readonly CommandHandler _commandHandler;
        private readonly IBotApiProvider _botProvider;
        private readonly char _prefix = '!';

        public Bot(IBotApiProvider botProvider)
        {
            _botProvider = botProvider;

            _commandHandler = new CommandHandler(new CommandsList());

            _commandHandler.RegisterCommand(new PingCommand());
            _commandHandler.RegisterCommand(new HelpCommand(_commandHandler.GetCommands()));
            _commandHandler.RegisterCommand(new ScheduleCommand());
            _commandHandler.RegisterCommand(new SetGroupCommand());
        }

        public void Process()
        {
            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            try
            {
                CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e);

                var commandTaskResult = _commandHandler.IsCommandCorrect(commandWithArgs);

                Logger.Info(commandTaskResult.ExecuteMessage);

                if (!commandTaskResult.IsSuccess)
                    return;

                var commandExecuteResult = _commandHandler.ExecuteCommand(commandWithArgs);

                if (!commandExecuteResult.IsSuccess)
                    Logger.Warning(commandExecuteResult.ExecuteMessage);

                var writeMessageResult =
                    _botProvider.WriteMessage(new SenderData(e.GroupId), commandExecuteResult.ExecuteMessage);

                Logger.Info(writeMessageResult.ExecuteMessage);
            }
            catch (Exception error)
            {
                Logger.Error(error.Message);
                _botProvider.Dispose();
                var result = _botProvider.Initialize();
                if (result.Exception != null)
                    Logger.Message(result.ExecuteMessage);

                Logger.Message(result.ExecuteMessage);
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}