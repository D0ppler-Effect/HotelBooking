using LiteDB;

namespace HotelBooking.Tests
{
	public class LiteDatabaseTestsBase : TestBase
	{
		[SetUp]
		public void SetupDatabase()
		{
			Database = new LiteDatabase(DatabaseName);
		}

		protected ILiteDatabase Database { get; set; }

		private const string DatabaseName = "TestDatabase.db";
	}
}
