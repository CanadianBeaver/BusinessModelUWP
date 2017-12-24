using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace BusinessModels.EntityGenerator
{
	internal static class ResourceHelper
	{
		private static string ReadResourceAsStrings(Assembly assembly, string resource)
		{
			var result = new StringBuilder();
			using (var stream = assembly.GetManifestResourceStream(resource))
			{
				using (var reader = new StreamReader(stream))
				{
					while (reader.Peek() >= 0)
					{
						var line = reader.ReadLine();
						if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith('*')) // we skip empty lines or lines with * at the beginning
							result.AppendLine(line.Trim());
					}
				}
			}
			return result.ToString();
		}

		public static string[] GetFileText(string fileName)
		{
			var result = ReadResourceAsStrings(typeof(ResourceHelper).GetTypeInfo().Assembly, fileName);
			return result.Split('\n', StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
