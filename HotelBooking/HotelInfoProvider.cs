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
			var searchResults = await _hotelsRepository.GetAllAsync(); // definitely a weak spot. TODO: reimagine data searching
			
			if (searchRequest.HasCoordinates)
			{
				searchResults = searchResults.Where(h => h.Details.Coordinates.IsWithinDistance(
						searchRequest.CenterPoint,
						searchRequest.SearchRadius))
					.ToList();
			}
			
			if(!string.IsNullOrEmpty(searchRequest.SearchText))
			{
				searchResults = await _hotelSearchEngine.SearchHotelsAsync(searchResults, searchRequest.SearchText);
			}

			return searchResults.Take(searchRequest.MaxSearchResults);
		}

		public async Task<HotelInfo> GetHotelDetailsAsync(Guid hotelId)
		{
			return await _hotelsRepository.GetHotelByIdAsync(hotelId);
		}

		public async Task<Guid> AddHotelAsync(HotelDetails details)
		{
			return await _hotelsRepository.CreateAsync(details);
		}

		private readonly IHotelsRepository _hotelsRepository;

		private readonly IHotelTextSearchEngine _hotelSearchEngine;
	}
}
