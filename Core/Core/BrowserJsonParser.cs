using System.Text.Json;
using Core.Common;
using Core.Enums;
using log4net;

namespace Core.Core;

public static class BrowserJsonParser
{
    private static ILog Log = LogManager.GetLogger(typeof(BrowserJsonParser));
	public static Browser GetBrowserType()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/TestConfig.json");
        string json = File.ReadAllText(path);
        var configRoot = JsonSerializer.Deserialize<TestConfig>(json);

        string browserString = configRoot?.Browser;

        if (Enum.TryParse<Browser>(browserString, true, out var browserEnum))
        {
            var browserMessage = $"Browser set to: {browserEnum}";
			Log.Info(browserMessage);

			return browserEnum;
		}

        var message = $"Browser '{browserString}' not recognized, defaulting to Edge.";
		Log.Warn(message);

		return Browser.Edge;
    }
}

