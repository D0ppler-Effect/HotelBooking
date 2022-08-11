using HotelBooking.Models;

namespace HotelBooking
{
	public interface IBookingReservationGateway
	{
		Task<BookingReservationResponse> CheckAvailabilityAndPutReservationAsync(BookingDetails desiredBooking);

		Task<bool> ConfirmBookingAsync(int reservationNumber);
	}
}
