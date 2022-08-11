namespace HotelBooking.Models
{
	public class BookingInfo
	{
		public BookingInfo(BookingDetails details, int reservationNumber)
		{
			Id = Guid.NewGuid();
			Details = details;
			ReservationNumber = reservationNumber;
		}

		public Guid Id { get; }

		public int ReservationNumber { get; }

		public BookingDetails Details { get; }
	}
}
