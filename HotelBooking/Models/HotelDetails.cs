namespace HotelBooking.Models
{
	public class HotelDetails
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public GeoCoordinates Coordinates { get; set; }

		public double Rating { get; set; }

		public decimal Price { get; set; }
	}
}
