using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Common
{
	public static class UrlParser
	{
		public static string GetBaseUrl()
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "Common/BaseUri.json");
			string json = File.ReadAllText(path);

			return JsonSerializer.Deserialize<string>(json)!;
		}
	}
}
