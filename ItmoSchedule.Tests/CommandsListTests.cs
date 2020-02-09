using System.Collections.Generic;
using ITMOSchedule.Bot.Commands;
using ITMOSchedule.Common;
using ItmoSchedule.Tests.Tools;
using NUnit.Framework;

namespace ItmoSchedule.Tests
{
    public class CommandsListTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExecuteAllAvailable_OnlyForAvailableResultWasReturned()
        {
            var list = new CommandsList();
            list.AddCommand(new CommandMocks.InputCheckerCommand("first"));
            list.AddCommand(new CommandMocks.InputCheckerCommand("second"));

            List<CommandExecuteResult> result = list.ExecuteAllAvailable(new CommandArgumentContainer("first"));

            Assert.AreEqual(1, result.Count);
            Assert.Pass();
        }
    }
}