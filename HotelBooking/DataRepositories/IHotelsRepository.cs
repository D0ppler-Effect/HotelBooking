using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IHotelsRepository
	{
		Task<Guid> CreateAsync(HotelDetails details);

		Task<HotelInfo> GetAsync(Guid id);

		Task<List<HotelInfo>> GetManyAsync(Func<HotelInfo, bool> searchFilter = null);
	}
}
