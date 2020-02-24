using System;
using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;

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
        public string[] Args { get; } = new string[0];

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            var result = string.Join(Environment.NewLine+Environment.NewLine,
                _commandList.Commands.ToList()
                    .Select(commands =>
                        $"{commands.Value.CommandName} : {commands.Value.Description}, {Environment.NewLine}args: " +
                        $"{string.Join(", ", commands.Value.Args.Select(s=>s))}"));

            _provider.WriteMessage(args.GroupId, result);
            return new CommandExecuteResult(true);
        }
    }
}