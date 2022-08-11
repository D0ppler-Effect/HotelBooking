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

		public Task<List<HotelInfo>> GetAllAsync()
		{
			return Task.FromResult(_collection.FindAll().ToList());
		}

		public Task<HotelInfo> GetHotelByIdAsync(Guid id)
		{
			var searchResult = _collection
				.Find(h => h.Id == id)
				.ToList();

			var result = searchResult.SingleOrDefault();

			return Task.FromResult(result);
		}
		
		private const string CollectionName = "hotels";

		private readonly ILiteCollection<HotelInfo> _collection;
	}
}
