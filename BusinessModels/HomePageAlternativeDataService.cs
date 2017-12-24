using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessModels.Contracts;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessModels.Contracts
{
	using BusinessModels.HomePageAlternativeDataServiceResults;

	public interface IHomePageAlternativeDataService
	{
		Task<Company[]> GetCompaniesAsync();
	}
}

namespace BusinessModels.HomePageAlternativeDataServiceResults
{
	public class Company
	{
		public Guid Id { get; set; }
		public string DisplayName { get; set; }
		public bool HasEmployees { get; set; }
		public int FemaleEmployees { get; set; }
		public int MaleEmployees { get; set; }
		public int TotalEmployees { get => this.FemaleEmployees + this.MaleEmployees; }
	}
}

namespace BusinessModels
{
	using BusinessModels.HomePageAlternativeDataServiceResults;

	public class HomePageAlternativeDataService : IHomePageAlternativeDataService
	{
		public Task<Company[]> GetCompaniesAsync()
		{
			using (var db = new DatabaseContext())
			{
				var result = db.Departments.Select(c => new Company
				{
					Id = c.Id,
					DisplayName = c.Name,
					FemaleEmployees = c.Employees.Count(e => e.Gender == DBConstants.Gender_Female),
					MaleEmployees = c.Employees.Count(e => e.Gender == DBConstants.Gender_Male),
				});
				return result.ToArrayAsync();
			}
		}
	}
}
