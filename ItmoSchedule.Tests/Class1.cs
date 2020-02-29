using System;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Tools;
using ItmoSchedule.VK;
using ITMOSchedule.VK;
using NUnit.Framework;

namespace ItmoSchedule.Tests
{
    public enum TestTypes
    {
        None,
        UnitTesting,
        IntegrationTesting,
        FlyByTheSeatOfYourPantsTesting
    }

    
    [TestFixture]
    public class WriteMessageValidChecker
    {
        private IWriteMessage _messagesWriter;

        public void InitializeBot()
        {
            _messagesWriter = new ConsoleWriteMessage();
            var vkAuth = new VkAuth();
            vkAuth.GetApi(new VkSettings());
            Bot bot = new Bot(_messagesWriter, new VkBotApiProvider(vkAuth, new ConsoleWriteMessage()));
        }

        [Test]
        public void CanWriteMessage()
        {
            _messagesWriter.
        }
    }
}
