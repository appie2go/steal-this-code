using AutoFixture;

namespace Dispatching.TestFixtures.DomainObjects
{
    public class LocationCustomization : ICustomization
    {
        private readonly Fixture _fixture = new Fixture();

        public void Customize(IFixture fixture)
        {
            fixture.Customize<Location>(x => x.FromFactory(Create));
        }

        private Location Create()
        {
            var @long = 1 + _fixture.Create<decimal>();
            var lat = 1 + _fixture.Create<decimal>();
            return new Location(@long, lat);
        }
    }
}
