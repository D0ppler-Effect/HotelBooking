using HotelBooking.Models;

namespace HotelBooking
{
	public class HotelFindRequestFactory : IHotelFindRequestFactory
	{
		public HotelFindRequestFactory(double searchDistance, int maxResults)
		{
			_searchDistance = searchDistance;
			_maxResults = maxResults;
		}

		public HotelFindRequest GetSearchRequest(
			double? centerPointLatitude, 
			double? centerPointLongitude, 
			string searchText)
		{
			GeoCoordinates centerPoint = null;
			if (centerPointLatitude.HasValue && centerPointLongitude.HasValue)
			{
				centerPoint = new GeoCoordinates(centerPointLatitude.Value, centerPointLongitude.Value);
			}

			var request = new HotelFindRequest
			{
				CenterPoint = centerPoint,
				SearchText = searchText,
				MaxSearchResults = _maxResults,
				SearchRadius = _searchDistance
			};

			return request; 
		}

		private readonly double _searchDistance;

		private readonly int _maxResults;
	}
}
