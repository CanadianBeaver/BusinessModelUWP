using System;
using Database;
using BusinessModels.EntityGenerator;

namespace BusinessModels
{
	public static class DatabaseInitializer
	{
		/// <summary>
		/// Check and create the database for debugging application
		/// </summary>
		public static void InitializeDatabase()
		{
			using (var dbContext = new DatabaseContext())
			{
				if (System.Diagnostics.Debugger.IsAttached) dbContext.Database.EnsureDeleted();
				if (dbContext.Database.EnsureCreated())
				{
					Random rng = new Random(1234567890);
					dbContext.GenerateDepartments(rng);
					dbContext.GenerateProjects(rng);
					dbContext.GenerateEmployees(rng);
					dbContext.GenerateProjectsToEmployees(rng);
				}
			}
		}
	}
}
