using System;
using Database.EntityGenerator;
using Database;

namespace BusinessModels
{
	public static class DatabaseInitializer
	{
		/// <summary>
		/// Initializing the database
		/// </summary>
		public static void InitializeDatabase()
		{
			using (var dbContext = new DatabaseContext())
			{
				if (System.Diagnostics.Debugger.IsAttached) dbContext.Database.EnsureDeleted();
				if (dbContext.Database.EnsureCreated())
				{
					dbContext.EnsureInitialized();
				}
			}
		}
	}
}
