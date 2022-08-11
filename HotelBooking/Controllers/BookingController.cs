using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class BookingController : ControllerBase
	{
		public BookingController(
			IBookingProvider bookingProvider, 
			IHotelInfoProvider hotelInfoProvider,
			ILogger<BookingController> logger)
		{
			_hotelInfoProvider = hotelInfoProvider;
			_bookingProvider = bookingProvider;
			_logger = logger;
		}

		[HttpPost]
		public async Task<ActionResult<BookingInfo>> CreateBooking(BookingDetails details)
		{
			try
			{
				var hotelDetails = await _hotelInfoProvider.GetHotelDetailsAsync(details.HotelId);
				if (hotelDetails == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, "A booking requested for non-existing hotel");
				}

				var bookingCreationResponse = await _bookingProvider.CreateBookingAsync(details);
				if (!bookingCreationResponse.IsSuccessful)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, bookingCreationResponse.ErrorMessage);
				}

				return bookingCreationResponse.Result;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to place a booking for hotel with id '{0}'", details.HotelId);
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		private readonly IBookingProvider _bookingProvider;

		private readonly IHotelInfoProvider _hotelInfoProvider;

		private readonly ILogger<BookingController> _logger;
	}
}
