using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IBookingsRepository
	{
		Task CreateAsync(BookingInfo info);

		Task<BookingInfo> GetAsync(Guid id);

		Task DeleteAsync(Guid bookingId);
	}
}
