using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IHotelsRepository
	{
		Task<Guid> Create(HotelDetails info);

		Task<HotelInfo> Get(Guid id);
	}
}
