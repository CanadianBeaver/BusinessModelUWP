using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class Project
	{
		[Key] public Guid Id { get; set; }
		[Required] public string Name { get; set; }

		public ICollection<ProjectToEmployee> ProjectToEmployees { get; set; }

	} 	
}
