using HotelBooking.Models;

namespace HotelBooking.Extensions
{
	public static class GeoExtensions
	{
		/// <summary>
		/// The distance formula is: √[(x₂ - x₁)² + (y₂ - y₁)²].
		/// This works for any two points in 2D space with coordinates (x₁, y₁) for the first point and (x₂, y₂) for the second point.
		/// TODO: make calculations accurate and use distance in meters, not abstract coordinate units
		/// </summary>
		public static bool IsWithinDistance(this GeoCoordinates point1, GeoCoordinates point2, double maxDistance)
		{
			var distance = Math.Sqrt(
				Math.Pow(point2.Latitude - point1.Latitude, 2) +
				Math.Pow(point2.Longitude - point1.Longitude, 2));

			return distance <= maxDistance;
		}
	}
}
