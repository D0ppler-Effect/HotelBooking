using HotelBooking.Models;

namespace HotelBooking;

public interface IHotelFindRequestFactory
{
	HotelFindRequest GetSearchRequest(
		double? centerPointLatitude,
		double? centerPointLongitude,
		string searchText);
}