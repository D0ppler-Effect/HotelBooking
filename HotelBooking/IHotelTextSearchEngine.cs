using HotelBooking.Models;

namespace HotelBooking
{
	public interface IHotelTextSearchEngine
	{
		Task<List<HotelInfo>> SearchHotelsAsync(IEnumerable<HotelInfo> hotelsCollection, string searchText);
	}
}
