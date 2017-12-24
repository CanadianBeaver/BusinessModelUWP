using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class Department 
	{
		[Key] public Guid Id { get; set; }
		[Required] public string Name { get; set; }

		public ICollection<Employee> Employees { get; set; }

	}
}
