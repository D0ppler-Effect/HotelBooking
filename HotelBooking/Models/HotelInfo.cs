namespace HotelBooking.Models
{
	public class HotelInfo
	{
		public HotelInfo(HotelDetails details)
		{
			Id = Guid.NewGuid();
			Details = details;
		}

		public Guid Id { get; }

		public HotelDetails Details { get; }
	}
}
