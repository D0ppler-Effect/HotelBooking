using HotelBooking.Models;

namespace HotelBooking;

public interface IHotelSearchRequestFactory
{
	HotelFindRequest GetSearchRequest(
		double? centerPointLatitude,
		double? centerPointLongitude,
		string searchText);
}