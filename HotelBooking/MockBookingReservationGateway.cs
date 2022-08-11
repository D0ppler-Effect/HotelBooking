using HotelBooking.Models;

namespace HotelBooking
{
	public class MockBookingReservationGateway : IBookingReservationGateway
	{
		public Task<BookingReservationResponse> CheckAvailabilityAndPutReservationAsync(BookingDetails desiredBooking)
		{
			var result = new BookingReservationResponse
			{
				Success = true,
				ReservationNumber = _random.Next()
			};

			return Task.FromResult(result);
		}

		public Task<bool> ConfirmBookingAsync(int reservationNumber)
		{
			return Task.FromResult(true);
		}

		private readonly Random _random = new Random();
	}
}
