using System.Collections.Generic;

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
    }
}