using AutoFixture;
using Dispatching.Cabs;
using Dispatching.TestFixtures.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Cabs.CabTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GoToTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Cab _cab;

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());
            _cab = _fixture.Create<Cab>();
        }

        [TestMethod]
        public void WhenDestinationIsCurrentLocation_ShouldThrowApplicationException()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
            _cab.GoTo(location);

            // Assert
            _cab.CurrentLocation.Should().Be(location);
        }

        [TestMethod]
        public void WhenDistinationChanges_CurrentLocationShouldChange()
        {
            // Arrange
            var location = _cab.CurrentLocation;

            // Act
            Action act = () => _cab.GoTo(location);

            // Assert
            act.Should().Throw<ApplicationException>("Already there...");
        }
    }
}
