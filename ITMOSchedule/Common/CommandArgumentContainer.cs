using System.Collections.Generic;

namespace ITMOSchedule.Common
{
    public class CommandArgumentContainer
    {
        public string CommandName { get; set; }

        public SenderData Sender { get; set; }

        public List<string> Arguments { get; set; }

        public CommandArgumentContainer(string commandName, SenderData sender, List<string> arguments)
        {
            CommandName = commandName;
            Sender = sender;
            Arguments = arguments;
        }
    }
}