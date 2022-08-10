namespace HotelBooking.Models
{
	public class BookingInfo
	{
		public BookingInfo(BookingDetails details)
		{
			Id = Guid.NewGuid();
			Details = details;
		}

		public Guid Id { get; }

		public BookingDetails Details { get; }
	}
}
