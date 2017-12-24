using System;
using System.Linq;
using Database;
using Database.Models;

namespace BusinessModels.EntityGenerator
{
	public static class Generator
	{
  	/// <summary>
		/// Generates data for Departments
		/// </summary>
		/// <param name="dbContext"></param>
		public static void GenerateDepartments(this DatabaseContext dbContext, Random rng)
		{
			var names = ResourceHelper.GetFileText(@"BusinessModels.EntityGenerator.Names.Departments.txt");
			var count = Math.Min(10, names.Length);
			for (int i = 0; i < count; i++)
				dbContext.Departments.Add(new Department()
				{
					Id = Guid.NewGuid(),
					Name = names[rng.Next(names.Length)],
				});
			dbContext.SaveChanges();
		}

		/// <summary>
		/// Generates data for Projects
		/// </summary>
		/// <param name="dbContext"></param>
		public static void GenerateProjects(this DatabaseContext dbContext, Random rng)
		{
			var names = ResourceHelper.GetFileText(@"BusinessModels.EntityGenerator.Names.Projects.txt");
			var count = Math.Min(25, names.Length);
			for (int i = 0; i < count; i++)
				dbContext.Projects.Add(new Project()
				{
					Id = Guid.NewGuid(),
					Name = names[rng.Next(names.Length)],
				});
			dbContext.SaveChanges();
		}

		/// <summary>
		/// Generates data for Employees
		/// </summary>
		/// <param name="dbContext"></param>
		public static void GenerateEmployees(this DatabaseContext dbContext, Random rng)
		{
			var womenNames = ResourceHelper.GetFileText(@"BusinessModels.EntityGenerator.Names.Women.txt");
			var menNames = ResourceHelper.GetFileText(@"BusinessModels.EntityGenerator.Names.Men.txt");
			var surNames = ResourceHelper.GetFileText(@"BusinessModels.EntityGenerator.Names.Surnames.txt");
			var departmentIDs = dbContext.Departments.Select(d => d.Id);
			foreach (var departmentId in departmentIDs)
			{
				int count = rng.Next(5, 15);
				for (int i = 0; i < count; i++)
				{
					int gender = rng.NextDouble() > 0.5 ? DBConstants.Gender_Male : DBConstants.Gender_Female;
					var firstName = gender == DBConstants.Gender_Male ? menNames[rng.Next(menNames.Length)] : womenNames[rng.Next(womenNames.Length)];
					var lastName = surNames[rng.Next(surNames.Length)];
					var middleName = rng.NextDouble() > 0.75 ? menNames[rng.Next(menNames.Length)] : null;
					dbContext.Employees.Add(new Employee()
					{
						Id = Guid.NewGuid(),
						DepartmentId = departmentId,
						FirstName = firstName,
						LastName = lastName,
						MiddleName = middleName,
						Gender = gender,
						WagePerHour = rng.Next(15, 35) + Math.Round(rng.NextDouble(), 2),
					});
				}
			}
			dbContext.SaveChanges();
		}

		/// <summary>
		/// Generates data for relation between Projects and Employees
		/// </summary>
		/// <param name="dbContext"></param>
		public static void GenerateProjectsToEmployees(this DatabaseContext dbContext, Random rng)
		{
			var projectIDs = dbContext.Projects.Select(d => d.Id);
			var employeeIDs = dbContext.Employees.Select(em => em.Id).ToArray();

			foreach (var projectId in projectIDs)
			{
				int count = rng.Next(10, 100);
				for (int i = 0; i < count; i++)
				{
					Guid employeeId = employeeIDs[rng.Next(employeeIDs.Length)];
					if (rng.NextDouble() > 0.25)
					{
						dbContext.ProjectsToEmployees.Add(new ProductionProject()
						{
							Id = Guid.NewGuid(),
							ProjectId = projectId,
							EmployeeId = employeeId,
							ProductionTime = TimeSpan.FromHours(rng.Next(1, 100)),
							IncomePerHour = rng.Next(25, 45) + Math.Round(rng.NextDouble(), 2),
							Efficiency = (int)Math.Round(rng.NextDouble() * 100, 0),
						});
					}
					else
					{
						dbContext.ProjectsToEmployees.Add(new ResearchProject()
						{
							Id = Guid.NewGuid(),
							ProjectId = projectId,
							EmployeeId = employeeId,
							ResearchTime = TimeSpan.FromHours(rng.Next(1, 100)),
						});
					}
				}
			}
			dbContext.SaveChanges();
		}
	}
}
