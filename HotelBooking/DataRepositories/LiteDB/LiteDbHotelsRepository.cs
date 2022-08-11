using HotelBooking.Extensions;
using HotelBooking.Models;
using LiteDB;

namespace HotelBooking.DataRepositories.LiteDB
{
	public class LiteDbHotelsRepository : IHotelsRepository
	{
		public LiteDbHotelsRepository(ILiteDatabase database)
		{
			_collection = database.GetCollection<HotelInfo>(CollectionName);
			_collection.EnsureIndex(h => h.Details.Name);
		}

		public Task<Guid> CreateAsync(HotelDetails details)
		{
			var newHotelInfo = new HotelInfo(details);

			_collection.Insert(newHotelInfo);

			return Task.FromResult(newHotelInfo.Id);
		}

		public Task<List<HotelInfo>> GetAsync(int maxResults, Func<HotelInfo, bool> searchFilter = null)
		{
			if (searchFilter == null)
			{
				return Task.FromResult(_collection.FindAll().Take(maxResults).ToList());
			}

			var searchResults = _collection
				.Find(h => searchFilter(h))
				.Take(maxResults)
				.ToList();

			return Task.FromResult(searchResults);
		}

		public Task<HotelInfo> GetHotelByIdAsync(Guid id)
		{
			var searchResult = _collection
				.Find(h => h.Id == id)
				.ToList();

			var result = searchResult.SingleOrDefault();

			return Task.FromResult(result);
		}

		public Task<List<HotelInfo>> FindHotelsByCoordinatesAsync(GeoCoordinates centerPoint, double searchRadius)
		{
			var searchResult = _collection
				.Find(h => h.Details.Coordinates.IsWithinDistance(centerPoint, searchRadius))
				.ToList();

			return Task.FromResult(searchResult);
		}

		private const string CollectionName = "hotels";

		private readonly ILiteCollection<HotelInfo> _collection;
	}
}
