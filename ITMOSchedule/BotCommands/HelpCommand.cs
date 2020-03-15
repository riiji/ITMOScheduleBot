﻿using System;
using System.Linq;
using ItmoSchedule.Abstractions;
using ItmoSchedule.BotFramework.CommandControllers;
using ItmoSchedule.Common;

namespace ItmoSchedule.BotCommands
{
    public class HelpCommand : IBotCommand
    {
        private readonly CommandsList _commandList;

        public HelpCommand(CommandsList commandList)
        {
            _commandList = commandList;
        }

        public string CommandName { get; } = "Help";
        public string Description { get; } = "Print all available commands";
        public string[] Args { get; } = new string[0];

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public TaskExecuteResult Execute(CommandArgumentContainer args)
        {
            var result = string.Join(Environment.NewLine+Environment.NewLine,
                _commandList.Commands.ToList()
                    .Select(commands =>
                        $"{commands.Value.CommandName} : {commands.Value.Description}, {Environment.NewLine}args: " +
                        $"{string.Join(", ", commands.Value.Args.Select(s=>s))}"));

            return new TaskExecuteResult(true, result);
        }
    }
}