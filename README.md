# Business Model in UWP applications

I would like to share my experience in developing the UWP applications based on Prism and Unity.

## Introduction

The [Universal Windows Platform](https://en.wikipedia.org/wiki/Universal_Windows_Platform) is a great idea by Microsoft (with "blackjack and technologies") that allows to develop application with one API and allows to reach a lot of devices such as Desktops, Xboxs, HoloLens and Surfaces. Also, SQLite is the recommended database for creating local storage in the UWP applications and Microsoft introduced the [Entity Framework Core](https://docs.microsoft.com/en-us/windows/uwp/data-access/sqlite-databases) for software developers. Basically, the development of UWP applications is similar to the development of WPF applications based on the MVVM template.

## Default UWP approach

With the classical approach the UWP application encapsulates visualization logic and business logic in the same place, as well as SQL queries or queries to the services. Developer has to create a page and code that is serving this page (code-behind). This approach is perfect for implementing the simple requirements and the simple business logic.

```HTML
<ListView x:Name="departmentsListView">
  <ListView.ItemTemplate>
    <DataTemplate>
      <StackPanel Orientation="Vertical">
        <StackPanel Orientation="Horizontal">
          <TextBlock Text="{Binding DisplayName}"/>
        </StackPanel>
        <StackPanel Orientation="Horizontal">
          <TextBlock>Male employees:</TextBlock>
          <TextBlock Text="{Binding MaleEmployees}"/>
        </StackPanel>
        <StackPanel Orientation="Horizontal">
          <TextBlock>Female employees:</TextBlock>
          <TextBlock Text="{Binding FemaleEmployees}"/>
        </StackPanel>
      </StackPanel>
    </DataTemplate>
  </ListView.ItemTemplate>
</ListView>
```

```cs
public sealed partial class DefaultPage
{
  public DefaultPage()
  {
    this.InitializeComponent();
  }

  protected override void OnNavigatedTo(NavigationEventArgs e)
  {
    base.OnNavigatedTo(e);
    using (var db = new DatabaseContext())
    {
      this.departmentsListView.ItemsSource = db.Departments
        .OrderByDescending(d => d.Employees.Count())
        .Take(5)
        .Select(d => new
        {
          Id = d.Id,
          DisplayName = d.Name,
          FemaleEmployees = d.Employees.Count(em => em.Gender == DBConstants.Gender_Female),
          MaleEmployees = d.Employees.Count(em => em.Gender == DBConstants.Gender_Male),
        })
        .ToArray();
    }
  }
}
```

## The Model

Let's start with a database that has tables for storing departments and employees:

```cs
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

```cs
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

## Default Model View approach


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
