using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class Employee 
	{
		[Key] public Guid Id { get; set; }
		[Required] public string FirstName { get; set; }
		[Required] public string LastName { get; set; }
		public string MiddleName { get; set; }
		public int Gender { get; set; }
		public double WagePerHour { get; set; } 

		public Guid DepartmentId { get; set; }
		public Department Department { get; set; }

		public ICollection<ProjectToEmployee> ProjectsToEmployee { get; set; }

	} 	
}
