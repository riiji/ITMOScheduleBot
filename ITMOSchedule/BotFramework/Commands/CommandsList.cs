using System.Collections.Generic;
using System.Threading.Tasks;
using ItmoSchedule.BotFramework.Exceptions;

namespace ItmoSchedule.BotFramework.Commands
{
    public class CommandsList
    {
        public readonly Dictionary<string, IBotCommand> Commands = new Dictionary<string, IBotCommand>();

        public void AddCommand(IBotCommand command)
        {
            Commands.Add(command.CommandName, command);
        }

        public Task<IBotCommand> GetCommand(string commandName)
        {
            return Commands.TryGetValue(commandName, out IBotCommand command) 
                ? Task.FromResult(command) 
                : Task.FromException<IBotCommand>(new BotValidException("Command not founded"));
        }
    }
}