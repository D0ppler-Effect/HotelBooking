using AutoFixture;
using FluentAssertions;
using HotelBooking.DataRepositories;
using HotelBooking.DataRepositories.LiteDB;
using HotelBooking.Models;

namespace HotelBooking.Tests
{
	[TestFixture]
	public class HotelsRepositoryTests : LiteDatabaseTestsBase
	{
		[SetUp]
		public void Setup()
		{
			HotelsRepository = new LiteDbHotelsRepository(Database);
		}

		[Test]
		public async Task ShouldInsertHotelIntoDatabaseAndReadItBack()
		{
			// arrange
			var hotelDetails = Fixture.Create<HotelDetails>();

			// act
			var hotelId = await HotelsRepository.CreateAsync(hotelDetails);
			var storedHotel = await HotelsRepository.GetHotelByIdAsync(hotelId);

			// assert
			storedHotel.Details.Address.Should().Be(hotelDetails.Address);
			storedHotel.Details.Description.Should().Be(hotelDetails.Description);
			storedHotel.Details.Name.Should().Be(hotelDetails.Name);
			storedHotel.Details.Price.Should().Be(hotelDetails.Price);
			storedHotel.Details.Rating.Should().Be(hotelDetails.Rating);
			storedHotel.Details.Coordinates.Longitude.Should().Be(hotelDetails.Coordinates.Longitude);
			storedHotel.Details.Coordinates.Latitude.Should().Be(hotelDetails.Coordinates.Latitude);
		}

		private IHotelsRepository HotelsRepository { get; set; }
	}
}