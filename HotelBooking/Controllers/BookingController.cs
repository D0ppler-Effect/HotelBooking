using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class BookingController : ControllerBase
	{
		public ActionResult Post(BookingDetails details)
		{
			throw new NotImplementedException();
		}
	}
}
