namespace ITMOSchedule.Bot.Commands
{
    public class CommandHandler
    {
        public void ExecuteCommand(string commandName, CommandArgumentContainer args)
        {
            var commandsList = new CommandsList();
            var command = commandsList.GetCommand(commandName);

            command.Execute(args);
        }
    }
}