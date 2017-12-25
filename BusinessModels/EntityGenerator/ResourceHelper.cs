using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BusinessModels.EntityGenerator
{
	internal static class ResourceHelper
	{
		private static List<string> ReadResourceAsStrings(Assembly assembly, string resource)
		{
			var result = new List<string>();
			using (var stream = assembly.GetManifestResourceStream(resource))
			{
				using (var reader = new StreamReader(stream))
				{
					while (reader.Peek() >= 0)
					{
						var line = reader.ReadLine();
						if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith('*')) // we skip empty lines or lines with * at the beginning
							result.Add(line.Trim());
					}
				}
			}
			return result;
		}

		public static List<string> GetFileText(string fileName)
		{
			var result = ReadResourceAsStrings(typeof(ResourceHelper).GetTypeInfo().Assembly, fileName);
			return result;
		}
	}
}
