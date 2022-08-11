using AutoFixture;
using FluentAssertions;
using HotelBooking.DataRepositories;
using HotelBooking.Models;
using Moq;

namespace HotelBooking.Tests
{
	[TestFixture]
	public class BookingProviderTests : TestBase
	{
		[SetUp]
		public void Setup()
		{
			var bookingRepoMock = new Mock<IBookingsRepository>();
			Fixture.Inject(bookingRepoMock.Object);
		}

		[Test]
		public async Task ShouldReturnUnsuccessfulResponseIfBookingUnavailable()
		{
			// arrange
			var bookingGatewayMock = new Mock<IBookingReservationGateway>();
			bookingGatewayMock
				.Setup(b => b.CheckAvailabilityAndPutReservationAsync(It.IsAny<BookingDetails>()).Result)
				.Returns(new BookingReservationResponse {Success = false});
			Fixture.Inject(bookingGatewayMock.Object);

			var bookingProvider = Fixture.Create<BookingProvider>();

			// act
			var result = await bookingProvider.CreateBookingAsync(Fixture.Create<BookingDetails>());

			// assert
			result.IsSuccessful.Should().BeFalse();
			result.Result.Should().BeNull();
			result.ErrorMessage.Should().Be("Could not reserve booking with external property");
		}

		[Test]
		public async Task ShouldReturnUnsuccessfulResponseInCaseOfBookingConfirmationError()
		{
			// arrange
			var bookingGatewayMock = new Mock<IBookingReservationGateway>();
			bookingGatewayMock
				.Setup(b => b.CheckAvailabilityAndPutReservationAsync(It.IsAny<BookingDetails>()).Result)
				.Returns(new BookingReservationResponse { Success = true });
			bookingGatewayMock
				.Setup(b => b.ConfirmBookingAsync(It.IsAny<int>()).Result)
				.Returns(false);
			Fixture.Inject(bookingGatewayMock.Object);

			var bookingProvider = Fixture.Create<BookingProvider>();

			// act
			var bookingRequest = Fixture.Create<BookingDetails>();
			var result = await bookingProvider.CreateBookingAsync(bookingRequest);

			// assert
			result.IsSuccessful.Should().BeFalse();
			result.Result.Should().BeNull();
		}
	}
}
