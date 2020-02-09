using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMOSchedule.Common
{
    //TODO: add sender info etc.
    public class CommandArgumentContainer
    {
        public List<String> Arguments { get; set; }

        public CommandArgumentContainer()
        {
            
        }

        public CommandArgumentContainer(params string[] arguments)
        {
            Arguments = arguments.ToList();
        }
    }
}