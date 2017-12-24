using System;
using Database;
using Database.Models;

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
					dbContext.CreateInitialData();
				}
			}
		}

		/// <summary>
		/// Generates the initial data 
		/// </summary>
		/// <param name="dbContext"></param>
		private static void CreateInitialData(this DatabaseContext dbContext)
		{
			#region Departments
			var departmentSales = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Sales",
			};
			var departmentIT = new Department
			{
				Id = Guid.NewGuid(),
				Name = "IT",
			};
			var departmentManagement = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Management",
			};
			var departmentAccounting = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Accounting",
			};
			var departmentSupport = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Technical Support Team",
			};
			var departmentMarketing = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Marketing",
			};
			var departmentPurchasing = new Department
			{
				Id = Guid.NewGuid(),
				Name = "Purchasing",
			}; 
			dbContext.Departments.AddRange(departmentSales, departmentIT, departmentManagement, 
				departmentAccounting, departmentSupport, departmentMarketing, departmentPurchasing);
			#endregion

			#region Employees
			var emp1 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentPurchasing,
				FirstName = "Jonathan",
				LastName = "Spencer",
				MiddleName = null,
				Gender = DBConstants.Gender_Male,
				WagePerHour = 12.5,
			};
			var emp2 = new Employee
			{
				Id = Guid.NewGuid(),
				Department= departmentMarketing,
				FirstName = "Amanda",
				LastName = "Clark",
				MiddleName = "Chester",
				Gender = DBConstants.Gender_Female,
				WagePerHour = 18.75,
			};
			var emp3 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentSupport,
				FirstName = "Vivian",
				LastName = "Tyson",
				MiddleName = "Wilson",
				Gender = DBConstants.Gender_Female,
				WagePerHour = 15.25,
			};
			var emp4 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentAccounting,
				FirstName = "James",
				LastName = "Armstrong",
				MiddleName = "Trevor",
				Gender = DBConstants.Gender_Male,
				WagePerHour = 14.85,
			};
			var emp5 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentManagement,
				FirstName = "Matthew",
				LastName = "Bulwer",
				MiddleName = "Ellery",
				Gender = DBConstants.Gender_Male,
				WagePerHour = 15.85,
			};
			var emp6 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentIT,
				FirstName = "Grimbald",
				LastName = "Herman",
				MiddleName = null,
				Gender = DBConstants.Gender_Male,
				WagePerHour = 17.0,
			};
			var emp7 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentSales,
				FirstName = "Stephen",
				LastName = "Coleman",
				MiddleName = null,
				Gender = DBConstants.Gender_Male,
				WagePerHour = 16.25,
			};
			var emp8 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentSales,
				FirstName = "Ignatius",
				LastName = "Bradshaw",
				MiddleName = null,
				Gender = DBConstants.Gender_Male,
				WagePerHour = 15.55,
			};
			var emp9 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentIT,
				FirstName = "Laurence",
				LastName = "Brooks",
				MiddleName = null,
				Gender = DBConstants.Gender_Male,
				WagePerHour = 15.0,
			};
			var emp10 = new Employee
			{
				Id = Guid.NewGuid(),
				Department = departmentSupport,
				FirstName = "Anastasia",
				LastName = "Buford",
				MiddleName = "Green",
				Gender = DBConstants.Gender_Female,
				WagePerHour = 16.25,
			};
			dbContext.Employees.AddRange(emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10);
			#endregion

			dbContext.SaveChanges();
		}

	}
}
