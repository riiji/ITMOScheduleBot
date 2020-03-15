﻿using System;
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
        private readonly IWriteMessage _messageWriter;

        public Bot(IBotApiProvider botProvider, IWriteMessage messageWriter)
        {
            _botProvider = botProvider;
            _messageWriter = messageWriter;

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
            TaskExecuteResult commandExecuteResult = new TaskExecuteResult(true);
            try
            {
                CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e.Text, e.GroupId);

                var commandTaskResult = _commandHandler.IsCommandCorrect(commandWithArgs);

                Logger.Info(commandTaskResult.ExecuteMessage);

                if (!commandTaskResult.IsSuccess)
                    return;

                commandExecuteResult = _commandHandler.ExecuteCommand(commandWithArgs);

                if (!commandExecuteResult.IsSuccess)
                    Logger.Warning(commandExecuteResult.ExecuteMessage);
            }
            catch (Exception error)
            {
                Logger.Error(error.Message);
            }

            var writeMessageResult = _messageWriter.WriteMessage(new SenderData(e.GroupId), commandExecuteResult.ExecuteMessage);

            Logger.Info(writeMessageResult.ExecuteMessage);
            if (writeMessageResult.GetException() != null)
            {
                _botProvider.Initialize();
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}