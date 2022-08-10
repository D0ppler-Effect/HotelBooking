using HotelBooking.Models;

namespace HotelBooking.DataRepositories.LiteDB
{
	public class LiteDbHotelsRepository : IHotelsRepository
	{
		public Task<Guid> Create(HotelDetails info)
		{
			throw new NotImplementedException();
		}

		public Task<HotelInfo> Get(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
