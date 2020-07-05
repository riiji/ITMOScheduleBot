using System.Collections.Generic;
using System.Linq;
using MessengerBotFramework.BotFramework;
using MessengerBotFramework.BotFramework.Exceptions;

namespace MessengerBotFramework.Common
{
    public class CommandArgumentContainer
    {
        public string CommandName { get; }
        public SenderData Sender { get; }
        public List<string> Arguments { get; }

        public CommandArgumentContainer(string commandName, SenderData sender, List<string> arguments)
        {
            CommandName = commandName;
            Sender = sender;
            Arguments = arguments;
        }

        public static CommandArgumentContainer Parse(BotEventArgs botArgs)
        {
            string[] commands = botArgs.Text.Split();

            var commandName = commands.FirstOrDefault() ?? throw new BotValidException("ParseCommand: commandName was null");

            //commandName = prefixCommand + commandName.ToLower();

            //skip command name
            IEnumerable<string> args = commands.Skip(1);

            return new CommandArgumentContainer(commandName, new SenderData(botArgs.GroupId), args.ToList());
        }
    }
}