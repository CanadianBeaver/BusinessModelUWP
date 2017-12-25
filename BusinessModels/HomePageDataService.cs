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
			public int TotalEmployees { get; set; }
		}

		public class EmployeeResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public int TotalProjects { get; set; }
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
				var departmentsResult = db.Departments
					.OrderByDescending(d => d.Employees.Count())
					.Take(5)
					.Select(d => new DepartmentResult
					{
						Id = d.Id,
						DisplayName = d.Name,
						FemaleEmployees = d.Employees.Count(e => e.Gender == DBConstants.Gender_Female),
						MaleEmployees = d.Employees.Count(e => e.Gender == DBConstants.Gender_Male),
					})
					.ToArrayAsync();

				var projectsResult = db.Projects
					.OrderByDescending(p => p.ProjectToEmployees.Count())
				  .Take(5)
					.Select(p => new ProjectResult
					{
						Id = p.Id,
						DisplayName = p.Name,
						TotalEmployees = p.ProjectToEmployees.Count(),
					})
					.ToArrayAsync();

				var employeesResult = db.Employees
					.OrderByDescending(em => em.ProjectsToEmployee.Count())
					.ThenBy(em => em.WagePerHour)
				  .Take(9)
					.Select(em => new EmployeeResult
					{
						Id = em.Id,
						DisplayName = this.CalculateDisplayNameForEmployee(em.FirstName, em.LastName, em.MiddleName),
						TotalProjects = em.ProjectsToEmployee.Count(),
						Wage = em.WagePerHour,
					})
					.ToArrayAsync();

				return new GetDataResult
				{
					Departments = await departmentsResult,
					Projects = await projectsResult,
					Employees = await employeesResult,
				};
			}
		}
	}
}
