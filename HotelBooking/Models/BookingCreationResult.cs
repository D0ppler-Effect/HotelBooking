namespace HotelBooking.Models
{
	public class BookingCreationResult
	{
		public BookingCreationResult(string errorMessage)
		{
			IsSuccessful = false;
			ErrorMessage = errorMessage;
		}

		public BookingCreationResult(BookingInfo result)
		{
			IsSuccessful = true;
			Result = result;
		}

		public bool IsSuccessful { get; }

		public string ErrorMessage { get; }

		public BookingInfo Result { get; }
	}
}
