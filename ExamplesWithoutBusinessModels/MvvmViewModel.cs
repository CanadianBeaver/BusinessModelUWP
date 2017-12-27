﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;
using Database.Models;

namespace ExamplesWithoutBusinessModels
{
	public sealed partial class MvvmViewModel : INotifyPropertyChanged
	{
		public class DepartmentResult
		{
			public Guid Id { get; set; }
			public string DisplayName { get; set; }
			public int FemaleEmployees { get; set; }
			public int MaleEmployees { get; set; }
			public int TotalEmployees { get => this.FemaleEmployees + this.MaleEmployees; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private ObservableCollection<DepartmentResult> departments;
		public ObservableCollection<DepartmentResult> Departments
		{
			get => this.departments;
			set
			{
				if (value != this.departments)
				{
					this.departments = value;
					this.NotifyPropertyChanged(); // nameof(this.Departments)
				}
			}
		}

		public MvvmViewModel()
		{
			using (var db = new DatabaseContext())
			{
				this.departments = new ObservableCollection<DepartmentResult>(db.Departments
					.OrderByDescending(d => d.Employees.Count())
					.Take(5)
					.Select(d => new DepartmentResult()
					{
						Id = d.Id,
						DisplayName = d.Name,
						FemaleEmployees = d.Employees.Count(em => em.Gender == DBConstants.Gender_Female),
						MaleEmployees = d.Employees.Count(em => em.Gender == DBConstants.Gender_Male),
					})
					.ToArray());
			}
		}
	}
}
