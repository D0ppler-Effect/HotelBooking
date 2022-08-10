using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IBookingsRepository
	{
		Task<Guid> CreateAsync(BookingDetails details);

		Task<BookingInfo> GetAsync(Guid id);
	}
}
