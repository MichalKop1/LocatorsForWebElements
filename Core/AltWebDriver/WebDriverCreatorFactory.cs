using Core.Core;


namespace Core.AltWebDriver;

public static class WebDriverCreatorFactory
{
	public static WebDriverCreator GetCreator()
	{
		Browser browser = BrowserJasonParser.GetBrowserType();

		return browser switch
		{
			Browser.Chrome => new ChromeWebDriverCreator(),
			Browser.Firefox => new FirefoxWebDriverCreator(),
			Browser.Edge => new EdgeWebDriverCreator(),
			_ => throw new NotSupportedException($"Browser {browser} is not supported.")
		};
	}
}
