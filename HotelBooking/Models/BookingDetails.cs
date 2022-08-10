namespace HotelBooking.Models
{
	public class BookingDetails
	{
		public Guid HotelId { get; set; }

		public DateTimeOffset CheckIn { get; set; }

		public DateTimeOffset CheckOut { get; set; }

		public string GuestName { get; set; }

		public int GuestsCount { get; set; }
	}
}
