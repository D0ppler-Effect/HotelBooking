using HotelBooking.Models;

namespace HotelBooking
{
	public interface IHotelProvider
	{
		Task<IEnumerable<HotelInfo>> SearchHotelsAsync(string searchText);

		Task<IEnumerable<HotelInfo>> FindHotelsNearbyAsync(GeoCoordinates centerPoint, double searchRadius);

		Task<HotelInfo> GetHotelDetailsAsync(Guid hotelId);
	}
}
