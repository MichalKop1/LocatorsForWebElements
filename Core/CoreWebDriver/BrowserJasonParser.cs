using System;
using System.IO;
using System.Text.Json;
using Core.Common;
using log4net;

namespace Core.Core;

public static class BrowserJasonParser
{
    private static ILog Log = LogManager.GetLogger(typeof(BrowserJasonParser));
	public static Browser GetBrowserType()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/TestConfig.json");
        string json = File.ReadAllText(path);
        var configRoot = JsonSerializer.Deserialize<TestConfigRoot>(json);

        string browserString = configRoot?.Browser ?? "Edge"; // set browser or deafult to Edge to avoid Warning message
        if (Enum.TryParse<Browser>(browserString, true, out var browserEnum))
        {
			return browserEnum;
		}

        var message = $"Browser '{browserString}' not recognized, defaulting to Edge.";
		Log.Warn(message);

		return Browser.Edge;
    }
}

