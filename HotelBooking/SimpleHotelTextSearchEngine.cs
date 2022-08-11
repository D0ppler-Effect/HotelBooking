using HotelBooking.Models;

namespace HotelBooking
{
	public class SimpleHotelTextSearchEngine : IHotelTextSearchEngine
	{
		public async Task<List<HotelInfo>> SearchHotelsAsync(IEnumerable<HotelInfo> hotelsCollection, string searchText)
		{
			var result = hotelsCollection.Where(h => 
				h.Details.Name.Contains(searchText) ||
				h.Details.Description.Contains(searchText));

			return await Task.FromResult(result.ToList());
		}
	}
}
