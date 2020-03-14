using System.Collections.Generic;
using ItmoSchedule.Abstractions;
using ItmoSchedule.Common;

namespace ItmoSchedule.BotFramework.CommandControllers
{
    public class CommandsList
    {
        public readonly Dictionary<string, IBotCommand> Commands = new Dictionary<string, IBotCommand>();

        public void AddCommand(IBotCommand command)
        {
            Commands.TryAdd(command.CommandName, command);
        }

        public TaskExecuteResult<IBotCommand> GetCommand(string commandName)
        {
            return Commands.TryGetValue(commandName, out IBotCommand command)
                ? new TaskExecuteResult<IBotCommand>(true, command)
                : new TaskExecuteResult<IBotCommand>(false, $"command {commandName} not founded", null);
        }
    }
}