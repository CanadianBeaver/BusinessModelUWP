using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
	public static class DBConstants
	{
		public const string DiscriminatorFieldName = "TypeOf"; // Default value is "Discriminator";

		// Discriminators for ProjectToEmployee
		public static class ProjecType
		{
			public const int Research = 1;
			public const int Production = 2;
		}

		// Genders 
		public const int Gender_Female = 0;
		public const int Gender_Male = 1;

	}
}
