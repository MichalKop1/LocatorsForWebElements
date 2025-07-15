using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Core.Enums;


namespace Core.Core;

public static class WebDriverFactory
{
	private static IWebDriver instance;

	public static LoggingWebDriver GetDriver(Browser browser)
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

		return new LoggingWebDriver(instance);
	}

	public static LoggingWebDriver GetDriver(Browser browser, DriverOptions options)
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

		return new LoggingWebDriver(instance);
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
