namespace HotelBooking.Models
{
	public class HotelInfo
	{
		public HotelInfo()
		{
		}

		public HotelInfo(HotelDetails details)
		{
			Id = Guid.NewGuid();
			Details = details;
		}

		public Guid Id { get; set; }

		public HotelDetails Details { get; set; }
	}
}
