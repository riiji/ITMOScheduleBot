﻿using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;

namespace ItmoSchedule.BotFramework.Commands.List
{
    public class PingCommand : IBotCommand
    {
        private readonly IBotApiProvider _provider;

        public PingCommand(IBotApiProvider provider)
        {
            _provider = provider ?? throw new BotValidException("Ping Command: Provider not founded");
        }

        public string CommandName { get; } = "Ping";

        public string Description { get; } = "Answer pong on ping message";
        public string[] Args { get; } = new string[0];

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            return new CommandExecuteResult(true, "Pong");
        }
    }
}