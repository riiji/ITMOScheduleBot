using System.Collections.Generic;

namespace ITMOSchedule.Common
{
    public class CommandArgumentContainer
    {
        public string CommandName { get; set; }

        public int GroupId { get; set; }

        public List<string> Arguments { get; set; }

        public CommandArgumentContainer(int groupId, List<string> arguments, string commandName)
        {
            GroupId = groupId;
            Arguments = arguments;
            CommandName = commandName;
        }
    }
}