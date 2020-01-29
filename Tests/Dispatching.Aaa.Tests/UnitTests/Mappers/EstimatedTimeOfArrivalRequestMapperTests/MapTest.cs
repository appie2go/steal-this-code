using AutoFixture;
using Dispatching.Aaa.Mappers;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa.Tests.UnitTests.Mappers.EstimatedTimeOfArrivalRequestMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private EstimatedTimeOfArrivalRequestMapper _sut;

        private DateTime _timeOfDeparture;
        private Kilometer _distance;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new EstimatedTimeOfArrivalRequestMapper();

            _timeOfDeparture = _fixture.Create<DateTime>();
            _distance = _fixture.Create<Kilometer>();
        }

        [TestMethod]
        public async Task WhenDistance_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_timeOfDeparture, _distance);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.Kilometers.Should().Be(_distance.ToDecimal());
        }

        [TestMethod]
        public async Task WhenTimeOfDeparture_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_timeOfDeparture, _distance);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.TimeOfDeparture.Should().Be(_timeOfDeparture);
        }

        public class TheirSchema
        { 
            public decimal Kilometers { get; set; }
            public DateTime TimeOfDeparture { get; set; }
        }
    }
}
