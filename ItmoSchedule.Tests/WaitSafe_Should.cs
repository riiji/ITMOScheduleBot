using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using ItmoSchedule.Tools.Extensions;
using NUnit.Framework;

namespace ItmoSchedule.Tests
{
    public class WaitSafe_Should
    {
        [SetUp]
        public void Setup()
        {
            _fakeFunction = A.Fake<Func<Task>>();

            A.CallTo(() => _fakeFunction.Invoke())
                .Invokes(() => Thread.Sleep(3000))
                .Returns(Task.FromException(new Exception()));

        }

        private Func<Task> _fakeFunction;
        
        [Test]
        public void WaitSafe_WhenException_ShouldReturnFailedTask()
        {
            var fakeFunctionTask = _fakeFunction.Invoke();
            fakeFunctionTask.WaitSafe();

            fakeFunctionTask.IsFaulted.Should().Be(true);
        }

        [Test]
        public void WaitSafe_WhenException_ShouldCompleted()
        {
            var fakeFunctionTask = _fakeFunction.Invoke();
            fakeFunctionTask.WaitSafe();

            fakeFunctionTask.IsCompleted.Should().Be(true);
        }

        [Test]
        public void WaitSafe_WhenException_ShouldntCompletedSuccessfully()
        {
            var fakeFunctionTask = _fakeFunction.Invoke();
            fakeFunctionTask.WaitSafe();

            fakeFunctionTask.IsCompletedSuccessfully.Should().Be(false);
        }



    }
}