﻿using System;
using MessengerBotFramework.Abstractions;
using MessengerBotFramework.BotFramework.CommandControllers;
using MessengerBotFramework.Common;

namespace MessengerBotFramework.BotFramework
{
    public class Bot : IDisposable
    {
        private readonly CommandHandler _commandHandler;
        private readonly IBotApiProvider _botProvider;
        private readonly char _prefix = '!';

        public Bot(IBotApiProvider botProvider, CommandsList commandsList)
        {
            _botProvider = botProvider;

            _commandHandler = new CommandHandler(commandsList);
        }

        public void Process()
        {
            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            try
            {
                CommandArgumentContainer commandWithArgs = CommandArgumentContainer.Parse(e);

                var commandTaskResult = _commandHandler.IsCommandCorrect(commandWithArgs);

                LoggerHolder.Log.Verbose(commandTaskResult.ExecuteMessage);

                if (!commandTaskResult.IsSuccess)
                    return;

                var commandExecuteResult = _commandHandler.ExecuteCommand(commandWithArgs);

                if (!commandExecuteResult.IsSuccess)
                    LoggerHolder.Log.Warning(commandExecuteResult.ExecuteMessage);

                var writeMessageResult =
                    _botProvider.WriteMessage(new SenderData(e.GroupId), commandExecuteResult.ExecuteMessage);

                LoggerHolder.Log.Verbose(writeMessageResult.ExecuteMessage);
            }
            catch (Exception error)
            {
                LoggerHolder.Log.Error(error.Message);

                LoggerHolder.Log.Information("Message handling failed, try restart bot provider");
                _botProvider.Dispose();
                Result result = _botProvider.Initialize();
                
                LoggerHolder.Log.Information(result.ExecuteMessage);
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}