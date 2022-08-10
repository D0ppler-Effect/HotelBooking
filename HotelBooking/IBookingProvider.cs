using HotelBooking.Models;

namespace HotelBooking
{
	public interface IBookingProvider
	{
		Task<BookingInfo> CreateBooking(BookingDetails details);
	}
}
