using FakeItEasy;
using FluentAssertions;
using ItmoSchedule.BotCommands;
using ItmoSchedule.Common;
using NUnit.Framework;

namespace ItmoSchedule.Tests.Commands_Should
{
    public class PingCommand_Should
    {
        [Test]
        public void PingCommand_ShouldReturnPong()
        {
            PingCommand pingCommand = new PingCommand();
            CommandArgumentContainer fakeContainer = A.Fake<CommandArgumentContainer>();
            pingCommand.Execute(fakeContainer).IsSuccess.Should().Be(true);
        }

    }
}