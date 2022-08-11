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

		public async Task<Guid> CreateAsync(HotelDetails details)
		{
			var newHotelInfo = new HotelInfo(details);

			_collection.Insert(newHotelInfo);

			return await Task.FromResult(newHotelInfo.Id);
		}

		public async Task<List<HotelInfo>> GetAsync(int maxResults, Func<HotelInfo, bool> searchFilter = null)
		{
			if (searchFilter == null)
			{
				return await Task.FromResult(_collection.FindAll().Take(maxResults).ToList());
			}

			var searchResults = _collection
				.Find(h => searchFilter(h))
				.Take(maxResults)
				.ToList();

			return await Task.FromResult(searchResults);
		}

		private const string CollectionName = "hotels";

		private readonly ILiteCollection<HotelInfo> _collection;
	}
}
