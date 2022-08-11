using HotelBooking.Models;
using LiteDB;

namespace HotelBooking.DataRepositories.LiteDB
{
	public class LiteDbBookingsRepository : IBookingsRepository
	{
		public LiteDbBookingsRepository(ILiteDatabase database)
		{
			_collection = database.GetCollection<BookingInfo>(CollectionName);
		}

		public async Task CreateAsync(BookingInfo info)
		{
			_collection.Insert(info);

			await Task.CompletedTask;
		}

		public async Task<BookingInfo> GetAsync(Guid bookingId)
		{
			var bookingInfo = _collection.Find(b => b.Id == bookingId).Single();

			return await Task.FromResult(bookingInfo);
		}

		public async Task DeleteAsync(Guid bookingId)
		{
			_collection.DeleteMany(b => b.Id == bookingId);

			await Task.CompletedTask;
		}
		
		private const string CollectionName = "bookings";

		private readonly ILiteCollection<BookingInfo> _collection;
	}
}
