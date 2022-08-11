using HotelBooking.DataRepositories;
using HotelBooking.Extensions;
using HotelBooking.Models;

namespace HotelBooking
{
	public class HotelInfoProvider : IHotelInfoProvider
	{
		public HotelInfoProvider(IHotelsRepository hotelsRepository, IHotelTextSearchEngine hotelSearchEngine)
		{
			_hotelsRepository = hotelsRepository;
			_hotelSearchEngine = hotelSearchEngine;
		}

		public async Task<IEnumerable<HotelInfo>> FindHotelsAsync(HotelFindRequest searchRequest)
		{
			List<HotelInfo> searchResults;

			// start with coordinates to narrow search field
			if (searchRequest.HasCoordinates)
			{
				var hotels = await _hotelsRepository.GetAsync(int.MaxValue);

				searchResults = hotels.Where(h => h.Details.Coordinates.IsWithinDistance(
						searchRequest.CenterPoint,
						searchRequest.SearchRadius))
					.ToList();

				// add text search criteria
				if (!string.IsNullOrEmpty(searchRequest.SearchText))
				{
					searchResults = await _hotelSearchEngine.SearchHotelsAsync(searchResults, searchRequest.SearchText);
				}
			}

			// if no coordinates provided, we'll try to use only text-based search
			else if(!string.IsNullOrEmpty(searchRequest.SearchText))
			{
				var hotels = await _hotelsRepository.GetAsync(int.MaxValue); // definitely a weak spot. TODO: reimagine text-based searching
				searchResults = await _hotelSearchEngine.SearchHotelsAsync(hotels, searchRequest.SearchText);
			}

			// ok, no filters provided, we'll return all we've got
			else
			{
				searchResults = await _hotelsRepository.GetAsync(searchRequest.MaxSearchResults);
			}

			return searchResults;
		}

		public async Task<HotelInfo> GetHotelDetailsAsync(Guid hotelId)
		{
			var queryResults = await _hotelsRepository.GetAsync(1,h => h.Id == hotelId);

			return queryResults.SingleOrDefault();
		}
		
		private readonly IHotelsRepository _hotelsRepository;

		private readonly IHotelTextSearchEngine _hotelSearchEngine;
	}
}
