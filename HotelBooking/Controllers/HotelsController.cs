using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class HotelsController : ControllerBase
	{
		//public HotelsController(IHotelProvider hotelProvider, ILogger<HotelsController> logger)
		//{
		//	_hotelProvider = hotelProvider;
		//	_logger = logger;
		//}

		[HttpGet("{hotelId}")]
		public async Task<ActionResult<HotelInfo>> Get(Guid hotelId)
		{
			try
			{
				var hotelDetails = await _hotelProvider.GetHotelDetailsAsync(hotelId);

				if (hotelDetails == null)
				{
					return StatusCode(StatusCodes.Status404NotFound);
				}

				return hotelDetails;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to get hotel details for id '{0}'", hotelId);
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<HotelInfo>>> Find(string? search)
		{
			try
			{
				var searchResults = await _hotelProvider.SearchHotelsAsync(search);
				
				return searchResults.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to find hotels using expression '{0}'", search);
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		private readonly IHotelInfoProvider _hotelProvider;

		private readonly ILogger<HotelsController> _logger;
	}
}
