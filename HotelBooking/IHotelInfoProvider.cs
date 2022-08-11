using HotelBooking.Models;

namespace HotelBooking
{
	public interface IHotelInfoProvider
	{
		Task<IEnumerable<HotelInfo>> FindHotelsAsync(HotelFindRequest searchRequest);

		Task<HotelInfo> GetHotelDetailsAsync(Guid hotelId);

		Task<Guid> AddHotelAsync(HotelDetails details);
	}
}
