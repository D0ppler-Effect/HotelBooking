using AutoFixture;
using FluentAssertions;
using HotelBooking.DataRepositories;
using HotelBooking.Models;
using Moq;

namespace HotelBooking.Tests
{
	[TestFixture]
	public class HotelProviderTests : TestBase
	{
		[Test]
		public async Task ShouldFindHotelByCoordinates()
		{
			// arrange
			Fixture.Inject(new GeoCoordinates(1, 1));
			Hotels = Fixture.Create<List<HotelInfo>>();
			var hotelToBeFound = Hotels.Last();
			hotelToBeFound.Details.Coordinates = new GeoCoordinates(100, 100);

			var repositoryMock = new Mock<IHotelsRepository>();
			repositoryMock
				.Setup(m => m.GetAllAsync().Result)
				.Returns(Hotels);
			Fixture.Inject(repositoryMock.Object);

			var searchEngineMock = new Mock<IHotelTextSearchEngine>();
			Fixture.Inject(searchEngineMock.Object);

			var provider = Fixture.Create<HotelInfoProvider>();

			// act
			var request = new HotelFindRequest()
			{
				CenterPoint = new GeoCoordinates(99, 99),
				MaxSearchResults = int.MaxValue,
				SearchRadius = 10
			};

			var searchResults = await provider.FindHotelsAsync(request);

			// assert
			var hotel = searchResults.Should().ContainSingle().Subject;
			hotel.Id.Should().Be(hotelToBeFound.Id);
		}

		private List<HotelInfo> Hotels { get; set; }
	}
}
