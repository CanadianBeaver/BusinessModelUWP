using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
	public struct DBConstants
	{
		public const string DiscriminatorFieldName = "TypeOf"; // Default value is "Discriminator";

		// Discriminators for ProjectToEmployee
		public const int ProjecType_Research = 1;
		public const int ProjecType_Production = 2;

		// Genders 
		public const int Gender_Female = 0;
		public const int Gender_Male = 1;

	}
}
