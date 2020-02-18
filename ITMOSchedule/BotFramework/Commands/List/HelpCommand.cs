﻿using System;
using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using static System.String;

namespace ItmoSchedule.BotFramework.Commands.List
{
    public class HelpCommand : IBotCommand
    {
        private readonly IBotApiProvider _provider;
        private readonly CommandsList _commandList;

        public HelpCommand(IBotApiProvider provider, CommandsList commandList)
        {
            _provider = provider;
            _commandList = commandList;
        }

        public string CommandName { get; } = "Help";
        public string Description { get; } = "Print all available commands";

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            var result = Join(Environment.NewLine,
                _commandList.Commands.ToList()
                    .Select(commands => $"{commands.Value.CommandName} : {commands.Value.Description}"));

            _provider.WriteMessage(args.GroupId, result);
            return new CommandExecuteResult(true);
        }
    }
}