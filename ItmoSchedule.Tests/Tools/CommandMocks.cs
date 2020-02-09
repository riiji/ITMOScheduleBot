using System;
using System.Linq;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;

namespace ItmoSchedule.Tests.Tools
{
    public class CommandMocks
    {
        public class InputCheckerCommand : ICommand
        {
            private readonly string _value;

            public InputCheckerCommand(String value)
            {
                _value = value;
            }

            public String CommandName => $"Check for {_value}";
            public String Description { get; } = nameof(InputCheckerCommand);
            public Boolean CanExecute(CommandArgumentContainer args)
            {
                return (args.Arguments.First() == _value);
            }

            public CommandExecuteResult Execute(CommandArgumentContainer args)
            {
                return new CommandExecuteResult(true);
            }
        }
    }
}