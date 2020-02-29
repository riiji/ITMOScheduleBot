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
        private readonly ItmoApiProvider _itmoProvider;

        public Bot(IBotApiProvider botProvider)
        {
            _botProvider = botProvider;

            _itmoProvider = new ItmoApiProvider();
        }

        public void Process()
        {
            _commandHandler = new CommandHandler(new CommandsList(), _botProvider, _itmoProvider);
            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            try
            {
                CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e.Text, e.GroupId).Result;

                if (!_commandHandler.IsCommandCorrect(commandWithArgs))
                    return;

                var commandExecuteTask = _commandHandler.ExecuteCommand(commandWithArgs);
                commandExecuteTask.WaitSafe();

                if (commandExecuteTask.IsFaulted)
                    Logger.Warning(commandExecuteTask.Exception.Message);
            }
            catch (Exception error)
            {
                Logger.Warning(error.Message);
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}