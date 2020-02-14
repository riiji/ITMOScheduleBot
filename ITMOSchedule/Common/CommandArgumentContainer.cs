using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMOSchedule.Common
{
    public class CommandArgumentContainer
    {
        public int GroupId { get; set; }

        public string CommandName { get; set; }

        public List<string> Arguments { get; set; }

        public CommandArgumentContainer(int groupId, List<string> arguments, string commandName)
        {
            GroupId = groupId;
            Arguments = arguments;
            CommandName = commandName;
        }

        public CommandArgumentContainer(List<string> arguments, string commandName)
        {
            Arguments = arguments;
            CommandName = commandName;
        }
    }
}