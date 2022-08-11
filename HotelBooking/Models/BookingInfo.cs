namespace HotelBooking.Models
{
	public class BookingInfo
	{
		public BookingInfo()
		{
		}

		public BookingInfo(BookingDetails details, int reservationNumber)
		{
			Id = Guid.NewGuid();
			Details = details;
			ReservationNumber = reservationNumber;
		}

		public Guid Id { get; set; }

		public int ReservationNumber { get; set; }

		public BookingDetails Details { get; set; }
	}
}
