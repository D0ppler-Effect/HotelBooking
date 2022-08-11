using HotelBooking.Models;

namespace HotelBooking.DataRepositories
{
	public interface IHotelsRepository
	{
		Task<Guid> CreateAsync(HotelDetails details);
		
		Task<List<HotelInfo>> GetAsync(int maxResults, Func<HotelInfo, bool> searchFilter = null);

		Task<HotelInfo> GetHotelByIdAsync(Guid id);

		Task<List<HotelInfo>> FindHotelsByCoordinatesAsync(GeoCoordinates centerPoint, double searchRadius);
	}
}
