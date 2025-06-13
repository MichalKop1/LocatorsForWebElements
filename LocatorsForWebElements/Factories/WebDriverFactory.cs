using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using LocatorsForWebElements.Helpers;

namespace LocatorsForWebElements.Factories;

public static class WebDriverFactory
{
	private static IWebDriver instance;
	public static IWebDriver GetDriver(Browser browser)
	{
		if (instance == null)
		{
			instance = browser switch
			{
				Browser.Chrome => new ChromeDriver(),
				Browser.Firefox => new FirefoxDriver(),
				Browser.Edge => new EdgeDriver(),
				_ => throw new ArgumentException("Unsuported browser")
			};
		}

		return instance;
	}

	public static IWebDriver GetDriver(Browser browser, DriverOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		if (instance == null)
		{
			instance = browser switch
			{
				Browser.Chrome => new ChromeDriver((ChromeOptions)options),
				Browser.Firefox => new FirefoxDriver((FirefoxOptions)options),
				Browser.Edge => new EdgeDriver((EdgeOptions)options),
				_ => throw new ArgumentException("Unsuported browser")
			};
		}

		return instance;
	}

	public static IWebDriver GetDriverParallel(Browser browser)
	{
		return browser switch
		{
			Browser.Chrome => new ChromeDriver(),
			Browser.Firefox => new FirefoxDriver(),
			Browser.Edge => new EdgeDriver(),
			_ => throw new ArgumentException("Unsupported browser")
		};
	}

	public static IWebDriver GetDriverParallel(Browser browser, DriverOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		return browser switch
		{
			Browser.Chrome => new ChromeDriver((ChromeOptions)options),
			Browser.Firefox => new FirefoxDriver((FirefoxOptions)options),
			Browser.Edge => new EdgeDriver((EdgeOptions)options),
			_ => throw new ArgumentException("Unsupported browser")
		};
	}

	public static void QuitDriver()
	{
		if (instance != null)
		{
			instance.Quit();
			instance = null;
		}
	}
}
