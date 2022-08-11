using HotelBooking.Models;

namespace HotelBooking
{
	public interface IBookingProvider
	{
		Task<BookingCreationResult> CreateBookingAsync(BookingDetails details);
	}
}
