using HotelBooking.DataRepositories;
using HotelBooking.DataRepositories.LiteDB;

namespace HotelBooking
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			// builder.Services.AddSingleton<IHotelsRepository, LiteDbHotelsRepository>();

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