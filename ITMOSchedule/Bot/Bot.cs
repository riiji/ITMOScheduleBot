using System.Linq;
using ITMOSchedule.Bot.Commands;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Common;

namespace ITMOSchedule.Bot
{
    public class Bot
    {
        private CommandHandler _commandHandler;
        private readonly IBotApiProvider _provider;

        public Bot(IBotApiProvider apiProvider)
        {
            _provider = apiProvider ?? throw new BotValidException("Api provider not founded");
        }

        public void Process()
        {
            _commandHandler = new CommandHandler(new CommandsList(), _provider);

            _provider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            CommandContainer commandWithArgs = Utilities.ParseCommand(e.Text).Result;

            if (!_commandHandler.IsCommandCorrect(commandWithArgs))
            {
                // TODO: Logger (user is so stupid to write correct command)
            }

            var commandExecuteTask = _commandHandler.ExecuteCommand(commandWithArgs.Name, new CommandArgumentContainer(e.GroupId, commandWithArgs.Args.ToList()));

            if (commandExecuteTask.IsFaulted)
            {
                // TODO: Logger (coder is so stupid to write correct code)
            }
        }
    }
}