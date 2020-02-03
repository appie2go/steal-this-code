using AutoFixture;
using Dispatching.Rides.Processes.SecondaryPorts;
using Dispatching.Specifications.TestContext;
using NSubstitute;
using System;

namespace Dispatching.Specifications.TestCases
{
    internal class Clock : TestCase<IProvideTime>
    {
        private DateTime _currentTime;

        public Clock()
        {
            var fixture = new Fixture();
            _currentTime = fixture.Create<DateTime>();
        }

        public Clock WithTime(DateTime time)
        {
            _currentTime = time;
            return this;
        }

        protected override void Apply(IProvideTime substitute)
        {
            substitute.GetCurrentTime().Returns(_currentTime);
        }
    }
}
