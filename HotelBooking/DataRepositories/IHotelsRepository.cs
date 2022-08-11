using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IHotelsRepository
	{
		Task<Guid> CreateAsync(HotelDetails details);
		
		Task<HotelInfo> GetHotelByIdAsync(Guid id);

		Task<List<HotelInfo>> GetAllAsync();
	}
}
