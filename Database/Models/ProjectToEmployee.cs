using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public abstract class ProjectToEmployee
	{
		[Key] public Guid Id { get; set; }

		public Guid ProjectId { get; set; }
		public Project Project { get; set; }

		public Guid EmployeeId { get; set; }
		public Employee Employee { get; set; }
	}

	public class ResearchProject : ProjectToEmployee
	{
		public TimeSpan ResearchTime { get; set; }
	}

	public class ProductionProject : ProjectToEmployee
	{
		public TimeSpan ProductionTime { get; set; }
		public double IncomePerHour { get; set; } 
		public int Efficiency { get; set; } // In percentages (0-100)
	}

}
