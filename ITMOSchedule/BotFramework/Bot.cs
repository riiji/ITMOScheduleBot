using System;
using System.Linq;
using ItmoSchedule.BotFramework.Commands;
using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.Extensions;
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
            _botProvider = botProvider ?? throw new BotValidException("Api provider not founded");
            _itmoProvider = new ItmoApiProvider();
        }

        public void Process()
        {
            _commandHandler = new CommandHandler(new CommandsList(), _botProvider, _itmoProvider);

            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e.Text, e.GroupId).Result;

            if (!_commandHandler.IsCommandCorrect(commandWithArgs))
            {
                Utilities.Log(Utilities.LogLevel.Info, $"Command {commandWithArgs.CommandName} isnt corrected");
                _botProvider.WriteMessage(commandWithArgs.GroupId, "invalid command args");
                return;
            }
            
            var commandExecuteTask = _commandHandler.ExecuteCommand(commandWithArgs);
            commandExecuteTask.WaitSafe();

            if (commandExecuteTask.IsFaulted)
                Utilities.Log(Utilities.LogLevel.Warning, $"Error: {commandExecuteTask.Exception}");
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}