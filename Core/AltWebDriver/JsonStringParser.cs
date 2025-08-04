using Core.Common;
using System.Text.Json;

namespace Core.AltWebDriver
{
	public static class JsonStringParser
	{
		public static TestConfigRoot GetConfig()
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/TestConfig.json");
			string json = File.ReadAllText(path);
			var configRoot = JsonSerializer.Deserialize<TestConfigRoot>(json);

			return configRoot;
		}
	}
}
