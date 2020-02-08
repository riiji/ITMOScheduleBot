using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandHandler
    {
        public void ExecuteCommand(string commandName, string[] args)
        {
            var command = CommandsList.GetCommand(commandName);

            command.Execute(args);
        }
    }
}