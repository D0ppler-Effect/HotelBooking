using HotelBooking.Models;

namespace HotelBooking
{
	public interface IHotelAvailabilityProvider
	{
		Task<bool> CheckAvailabilityAndReserveBooking(BookingDetails desiredBooking);
	}
}
