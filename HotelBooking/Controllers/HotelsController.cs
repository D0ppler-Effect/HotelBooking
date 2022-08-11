using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class HotelsController : ControllerBase
	{
		public HotelsController(
			IHotelInfoProvider hotelInfoProvider,
			IHotelFindRequestFactory searchRequestFactory,
			ILogger<HotelsController> logger)
		{
			_hotelInfoProvider = hotelInfoProvider;
			_hotelSearchRequestFactory = searchRequestFactory;
			_logger = logger;
		}

		[HttpGet("{hotelId}")]
		public async Task<ActionResult<HotelInfo>> Get(Guid hotelId)
		{
			try
			{
				var hotelDetails = await _hotelInfoProvider.GetHotelDetailsAsync(hotelId);

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
		public async Task<ActionResult<List<HotelInfo>>> Find(string search, double? lat, double? lon)
		{
			try
			{
				if (lat == null ^ lon == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, $"Incomplete coordinates passed: lat:'{lat}' lon:'{lon}'");
				}

				var searchRequest = _hotelSearchRequestFactory.GetSearchRequest(lat, lon, search);

				var searchResults = await _hotelInfoProvider.FindHotelsAsync(searchRequest);
				
				return searchResults.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to find hotels using search text '{0}', latitude:'{1}', longitude:'{2}'", search, lat, lon);
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
		
		private readonly IHotelInfoProvider _hotelInfoProvider;

		private readonly IHotelFindRequestFactory _hotelSearchRequestFactory;

		private readonly ILogger<HotelsController> _logger;
	}
}
