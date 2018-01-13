# Business Model in UWP application

Implementation of business logic (domain logic) in a UWP application based on Prism and Unity

Let's start with a database that has tables for storing departments and employees:

```Charp
namespace Database.Models
{
	public class Department 
	{
		[Key] public Guid Id { get; set; }
		[Required] public string Name { get; set; }
		public ICollection<Employee> Employees { get; set; }
	}

	public class Employee 
	{
		[Key] public Guid Id { get; set; }
		[Required] public string FirstName { get; set; }
		[Required] public string LastName { get; set; }
		public string MiddleName { get; set; }
		public int Gender { get; set; }
		public Guid DepartmentId { get; set; }
		public Department Department { get; set; }
	} 	

	public struct DBConstants
	{
		// Genders 
		public const int Gender_Female = 0;
		public const int Gender_Male = 1;
	}
}
```

```Charp
namespace Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"Data Source=database.db");
		}

	}
}
```

## MVVM

Model, View, View Model, Business Model,

### Preparation

Creating projects

## Prism and Unity

Dependency Injection, 

## Model

Entity Framework Core, DatabaseContextLoggerProvider,

## Business Model 

...

## View Model

...

## Animation

...