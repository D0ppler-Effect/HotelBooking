using HotelBooking.DataRepositories;
using HotelBooking.DataRepositories.LiteDB;
using HotelBooking.Settings;

namespace HotelBooking
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var applicationSettings = builder.Configuration.Get<AppSettings>();

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddSingleton<IHotelsRepository, LiteDbHotelsRepository>();
			builder.Services.AddSingleton<IBookingsRepository, LiteDbBookingsRepository>();

			builder.Services.AddTransient<IHotelInfoProvider, HotelInfoProvider>();
			builder.Services.AddTransient<IBookingProvider, BookingProvider>();

			builder.Services.AddTransient<IHotelTextSearchEngine, SimpleHotelTextSearchEngine>();

			builder.Services.AddTransient<IBookingReservationGateway, MockBookingReservationGateway>();
			builder.Services.AddTransient<IHotelFindRequestFactory>(x=> new HotelFindRequestFactory(
				applicationSettings.Application.HotelSearchDistance,
				applicationSettings.Application.MaxSearchResults));

			var app = builder.Build();

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseRouting();

			app.MapControllers();

			app.Run();
		}
	}
}