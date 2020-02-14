using System.Linq;
using ITMOSchedule.Bot.Commands;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Common;
using ItmoScheduleApiWrapper;

namespace ITMOSchedule.Bot
{
    //TODO:; dispose
    public class Bot
    {
        private CommandHandler _commandHandler;
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public Bot(IBotApiProvider botProvider)
        {
            _botProvider = botProvider ?? throw new BotValidException("Api provider not founded");
            _itmoProvider = new ItmoApiProvider();
        }

        public void Process()
        {
            _commandHandler = new CommandHandler(new CommandsList(), _botProvider, _itmoProvider);

            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            CommandArgumentContainer commandWithArgs = Utilities.ParseCommand(e.Text).Result;
            
            if (!_commandHandler.IsCommandCorrect(commandWithArgs))
            {
                // TODO: Logger (user is so stupid to write correct command)
            }

            var commandExecuteTask = _commandHandler.ExecuteCommand(commandWithArgs);
            commandExecuteTask.WaitSafe();

            if (commandExecuteTask.IsFaulted)
            {
                // TODO: Logger (coder is so stupid to write correct code)
            }
        }
    }
}