using System;
using System.Linq;
using Database;
using Database.Models;
using Windows.UI.Xaml.Navigation;

namespace ExamplesWithoutBusinessModels
{
	public sealed partial class DefaultPage
	{
		public DefaultPage()
		{
			this.InitializeComponent();
		}

		public class DepartmentResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public int FemaleEmployees { get; set; }
			public int MaleEmployees { get; set; }
			public int TotalEmployees { get => this.FemaleEmployees + this.MaleEmployees; }
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			using (var db = new DatabaseContext())
			{
				this.departmentsListView.ItemsSource = db.Departments
					.OrderByDescending(d => d.Employees.Count())
					.Take(5)
					.Select(d => new DepartmentResult()
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
}
