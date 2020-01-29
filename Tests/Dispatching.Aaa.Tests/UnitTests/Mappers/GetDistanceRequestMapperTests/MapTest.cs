using AutoFixture;
using Dispatching.Aaa.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa.Tests.UnitTests.Mappers.GetDistanceRequestMapperTests
{
    [TestClass]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private GetDistanceRequestMapper _sut;

        private Location _from;
        private Location _to;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetDistanceRequestMapper();

            _from = _fixture.Create<Location>();
            _to = _fixture.Create<Location>();
        }

        [TestMethod]
        public async Task WhenFromLatitude_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_from, _to);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.Latitude.Should().Be(_from.Latitude);
        }

        [TestMethod]
        public async Task WhenFromLongitude_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_from, _to);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.Longitude.Should().Be(_from.Longitude);
        }

        [TestMethod]
        public async Task WhenToLatitude_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_from, _to);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.DestinationLatitude.Should().Be(_to.Latitude);
        }

        [TestMethod]
        public async Task WhenToLongitude_ShouldBeMappedIntoTheirDataStructure()
        {
            // Act
            var actual = _sut.Map(_from, _to);

            // Assert
            var deserializedObject = await actual.ReadAsAsync<TheirSchema>();
            deserializedObject.DestinationLongitude.Should().Be(_to.Longitude);
        }


        public class TheirSchema
        {
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public decimal DestinationLongitude { get; set; }
            public decimal DestinationLatitude { get; set; }
        }
    }
}
