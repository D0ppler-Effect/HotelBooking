using AutoFixture;

namespace HotelBooking.Tests
{
	public class TestBase
	{
		[SetUp]
		public void SetupFixture()
		{
			Fixture = new Fixture();
		}

		protected Fixture Fixture { get; set; }
	}
}
