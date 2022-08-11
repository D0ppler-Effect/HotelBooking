namespace HotelBooking.Models
{
	// TODO: implement results pagination
	public class HotelFindRequest
	{
		public string SearchText { get; set; }

		public GeoCoordinates CenterPoint { get; set; }

		public double SearchRadius { get; set; }

		public int MaxSearchResults { get; set; }

		public bool HasCoordinates => CenterPoint != null && SearchRadius > 0;
	}
}
