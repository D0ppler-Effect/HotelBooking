using HotelBooking.DataRepositories;
using HotelBooking.Models;

namespace HotelBooking
{
	public class BookingProvider : IBookingProvider
	{
		public BookingProvider(
			IBookingsRepository bookingsRepository,
			IBookingReservationGateway bookingReservationGateway)
		{
			_bookingsRepository = bookingsRepository;
			_bookingReservationGateway = bookingReservationGateway;
		}

		public async Task<BookingCreationResult> CreateBookingAsync(BookingDetails details)
		{
			// assume we're calling an external API to check online if desired booking is available
			// also, assume that external API is also responsible for reservations and actual communication with property

			var reservationResponse = await _bookingReservationGateway.CheckAvailabilityAndPutReservationAsync(details);
			if (!reservationResponse.Success)
			{
				return new BookingCreationResult("Could not reserve booking with external property");
			}

			var bookingInfo = new BookingInfo(details, reservationResponse.ReservationNumber);
			await _bookingsRepository.CreateAsync(bookingInfo);

			var confirmationResult = await _bookingReservationGateway.ConfirmBookingAsync(bookingInfo.ReservationNumber);
			if (confirmationResult)
			{
				return new BookingCreationResult(bookingInfo);
			}
			else
			{
				await _bookingsRepository.DeleteAsync(bookingInfo.Id);
				return new BookingCreationResult(
					$"Could not confirm booking with external property. Booking id:'{bookingInfo.Id}', reservation no:'{bookingInfo.ReservationNumber}");
			}
		}

		private readonly IBookingReservationGateway _bookingReservationGateway;

		private readonly IBookingsRepository _bookingsRepository;
	}
}
