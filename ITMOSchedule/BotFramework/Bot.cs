using System;
using ItmoSchedule.BotFramework.Commands;
using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.Extensions;
using ItmoSchedule.Tools;
using ITMOSchedule.VK;
using ItmoScheduleApiWrapper;

namespace ItmoSchedule.BotFramework
{
    public class Bot : IDisposable
    {
        private CommandHandler _commandHandler;
        private readonly IBotApiProvider _botProvider;

        public Bot(IBotApiProvider botProvider)
        {
            _botProvider = botProvider;
        }

        public void Process()
        {
            _commandHandler = new CommandHandler(new CommandsList(), _botProvider);
            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            try
            {
                CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e.Text, e.GroupId);

                if (!_commandHandler.IsCommandCorrect(commandWithArgs))
                    return;

                //TODO: log with info level executing
                Logger.Info(commandWithArgs.ToString());
                var commandExecuteTask = _commandHandler.ExecuteCommand(commandWithArgs);
                commandExecuteTask.WaitSafe();

                if (commandExecuteTask.IsFaulted)
                    Logger.Warning(commandExecuteTask.Exception.Message);
            }
            catch (Exception error)
            {
                Logger.Error(error.Message);
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}