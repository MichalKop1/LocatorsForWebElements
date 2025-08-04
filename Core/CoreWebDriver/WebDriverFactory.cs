using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Core.AltWebDriver;

namespace Core.Core;



public static class WebDriverFactory
{
	public static IWebDriver GetDriver(DriverOptions? options = null)
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
