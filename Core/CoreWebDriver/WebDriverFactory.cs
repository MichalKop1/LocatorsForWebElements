using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Core.Core;

namespace Core.Core;

public static class  WebDriverFactory
{
	public static ChromeDriver GetChromeDriver(ChromeOptions? options = null)
	{
		return options == null ? new ChromeDriver() : new ChromeDriver(options);
	}

	public static FirefoxDriver GetFirefoxDriver(FirefoxOptions? options = null)
	{
		return options == null ? new FirefoxDriver() : new FirefoxDriver(options);
	}

	public static EdgeDriver GetEdgeDriver(EdgeOptions? options = null)
	{
		return options == null ? new EdgeDriver() : new EdgeDriver(options);
	}
}

public static class WebDriver2
{
	public static IWebDriver GetDriver(DriverOptions? options)
	{
		Browser browser = BrowserJasonParser.GetBrowserType();

		return browser switch
		{
			Browser.Chrome => new ChromeDriver((ChromeOptions)options!),
			Browser.Firefox => new FirefoxDriver((FirefoxOptions)options!),
			Browser.Edge => new EdgeDriver((EdgeOptions)options!),
			_ => throw new NotSupportedException($"Browser {browser} is not supported.")
		};
	}
}
