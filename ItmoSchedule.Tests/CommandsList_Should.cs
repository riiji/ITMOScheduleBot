using FakeItEasy;
using FluentAssertions;
using MessengerBotFramework.Abstractions;
using MessengerBotFramework.BotFramework.CommandControllers;
using NUnit.Framework;

namespace ItmoSchedule.Tests
{
    public class CommandsList_Should
    {
        [SetUp]
        public void Setup()
        {
            _commands = new CommandsList();

            _fakeCommand = A.Fake<IBotCommand>();
            A.CallTo(() => _fakeCommand.CommandName).Returns("someCommand");
        }

        private CommandsList _commands;
        private IBotCommand _fakeCommand;
        
        [Test]
        public void RegisterCommand_Correctly()
        {
            _commands.Add(_fakeCommand);
            _commands.GetCommand("someCommand").IsSuccess.Should().Be(true);
            _commands.GetCommand("someCommand").Value.Should().Be(_fakeCommand);
        }

        [Test]
        public void RegisterCommand_ShouldIgnoreSameCommands()
        {
            _commands.Add(_fakeCommand);
            _commands.Add(_fakeCommand);

            _commands.GetCommand("someCommand").IsSuccess.Should().Be(true);
            _commands.GetCommand("someCommand").Value.Should().Be(_fakeCommand);
        }

        [Test]
        public void RegisterCommand_ShouldGetCorrectCommand_WhenManyCommands()
        {
            A.CallTo(() => _fakeCommand.CommandName).Returns("someCommand1").Once();
            A.CallTo(() => _fakeCommand.CommandName).Returns("someCommand2").Once();
            A.CallTo(() => _fakeCommand.CommandName).Returns("someCommand3").Once();
            A.CallTo(() => _fakeCommand.CommandName).Returns("someCommand4").Once();

            _commands.Add(_fakeCommand);
            _commands.Add(_fakeCommand);
            _commands.Add(_fakeCommand);
            _commands.Add(_fakeCommand);

            _commands.GetCommand("someCommand3").IsSuccess.Should().Be(true);
            _commands.GetCommand("someCommand3").Value.Should().Be(_fakeCommand);
            
            _commands.GetCommand("someCommand1").IsSuccess.Should().Be(true);
            _commands.GetCommand("someCommand1").Value.Should().Be(_fakeCommand);
        }

        [Test]
        public void RegisterCommand_ReturnsNull_WhenGetInvalidCommand()
        {
            _commands.GetCommand("someCommand").IsSuccess.Should().Be(false);
            _commands.GetCommand("someCommand").Value.Should().BeNull();
        }
    }
}