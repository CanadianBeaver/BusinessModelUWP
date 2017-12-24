using Prism.Windows.Mvvm;
using Prism.Windows;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using BusinessModels;

namespace UWPApplication.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{

		private HomePageDataService.DepartmentResult[] departments;
		public HomePageDataService.DepartmentResult[] Departments
		{
			get => this.departments;
			private set => SetProperty(ref this.departments, value);
		}

		private HomePageDataService.ProjectResult[] projects;
		public HomePageDataService.ProjectResult[] Projects
		{
			get => this.projects;
			private set => SetProperty(ref this.projects, value);
		}

		private HomePageDataService.EmployeeResult[] employees;
		public HomePageDataService.EmployeeResult[] Employees
		{
			get => this.employees;
			private set => SetProperty(ref this.employees, value);
		}

		public HomePageViewModel() { }

		private readonly IHomePageDataService dataService;

		[InjectionConstructor]
		public HomePageViewModel(IHomePageDataService dataService)
		{
			this.dataService = dataService;
		}

		public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);
			var result = await this.dataService.GetDataAsync();
			this.Departments = result.Departments;
			this.Projects = result.Projects;
			this.Employees = result.Employees;
		}
	}
}
