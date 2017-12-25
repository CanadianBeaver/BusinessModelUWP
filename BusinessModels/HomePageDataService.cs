using System;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessModels
{
	public interface IHomePageDataService
	{
		Task<HomePageDataService.GetDataResult> GetDataAsync();
	}

	public class HomePageDataService : IHomePageDataService
	{
		public class GetDataResult
		{
			public DepartmentResult[] Departments;
			public ProjectResult[] Projects;
			public EmployeeResult[] Employees;
		}

		public class DepartmentResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public int FemaleEmployees { get; set; }
			public int MaleEmployees { get; set; }
			public int TotalEmployees { get => this.FemaleEmployees + this.MaleEmployees; }
		}

		public class ProjectResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public double TotalIncome { get; set; }
		}

		public class EmployeeResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public double Wage { get; set; }
		}

		private string CalculateDisplayNameForEmployee(string firstName, string lastName, string middleName = null)
		{
			string result;

			if (!string.IsNullOrWhiteSpace(firstName)) result = $"{firstName.Trim()} ";
			else result = string.Empty;

			if (!string.IsNullOrWhiteSpace(middleName)) result += $"{middleName.TrimStart()[0]}. ";

			if (!string.IsNullOrWhiteSpace(lastName)) result += $"{lastName.TrimStart()}";
			else result = result.TrimEnd();

			return result;
		}

		public async Task<HomePageDataService.GetDataResult> GetDataAsync()
		{
			using (var db = new DatabaseContext())
			{
				var departmentsResult = db.Departments.Select(c => new DepartmentResult
				{
					Id = c.Id,
					DisplayName = c.Name,
					FemaleEmployees = c.Employees.Count(e => e.Gender == DBConstants.Gender_Female),
					MaleEmployees = c.Employees.Count(e => e.Gender == DBConstants.Gender_Male),
				})
				.Take(5);
				var projectsResult = db.ProjectsToEmployees.OfType<ProductionProject>()
					.GroupBy(p => new { p.ProjectId, p.Project.Name })
					.Select(p => new ProjectResult
					{
						Id = p.Key.ProjectId,
						DisplayName = p.Key.Name,
						TotalIncome = p.Sum(c => c.IncomePerHour * c.ProductionTime.TotalHours)
					})
					.Take(5);
				var employeesResult = db.Employees.Select(c => new EmployeeResult
				{
					Id = c.Id,
					DisplayName = this.CalculateDisplayNameForEmployee(c.FirstName, c.LastName, c.MiddleName),
					Wage = c.WagePerHour,
				})
				.Take(10);
				return new GetDataResult
				{
					Departments = await departmentsResult.ToArrayAsync(),
					Projects = await projectsResult.ToArrayAsync(),
					Employees = await employeesResult.ToArrayAsync(),
				};
			}
		}
	}
}
