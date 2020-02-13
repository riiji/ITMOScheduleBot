using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMOSchedule.Common
{
    public class CommandArgumentContainer
    {
        public int GroupId { get; set; }

        public List<String> Arguments { get; set; }

        public CommandArgumentContainer(int groupId, List<string> arguments)
        {
            GroupId = groupId;
            Arguments = arguments;
        }

        public CommandArgumentContainer(List<string> arguments)
        {
            Arguments = arguments;
        }
    }
}