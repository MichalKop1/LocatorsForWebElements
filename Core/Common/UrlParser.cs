using System.Text.Json;

namespace Core.Common;

public static class UrlParser
{
	public static string GetBaseUrl()
	{
		string path = Path.Combine(Directory.GetCurrentDirectory(), "Common/BaseUri.json");
		string json = File.ReadAllText(path);

		return JsonSerializer.Deserialize<string>(json)!;
	}
}
