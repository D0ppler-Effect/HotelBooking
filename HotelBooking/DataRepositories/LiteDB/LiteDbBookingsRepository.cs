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

		public async Task<Guid> CreateAsync(BookingDetails details)
		{
			var newBookingInfo = new BookingInfo(details);

			_collection.Insert(newBookingInfo);

			return await Task.FromResult(newBookingInfo.Id);
		}

		public async Task<BookingInfo> GetAsync(Guid id)
		{
			var bookingInfo = _collection.Find(b => b.Id == id).Single();

			return await Task.FromResult(bookingInfo);
		}
		
		private const string CollectionName = "bookings";

		private readonly ILiteCollection<BookingInfo> _collection;
	}
}
