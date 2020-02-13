using ITMOSchedule.Common;

namespace ITMOSchedule.Bot.Commands
{
    public class CommandHandler
    {
        private readonly CommandsList _commands;

        public CommandHandler(CommandsList commands)
        {
            _commands = commands;
        }

        public bool IsCommandExisted(string commandName)
        {
            return _commands.IsCommandExisted(commandName);
        }

        public void ExecuteCommand(string commandName, CommandArgumentContainer args)
        {
            var command = _commands.GetCommand(commandName);

            command.Execute(args);
        }



    }
}