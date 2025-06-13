using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace LocatorsForWebElements.Helpers;

public class WebDriverBuilder
{
	private bool headless;
	private bool incognito;
	private bool maximized;
	private bool minimized;

	public WebDriverBuilder Headless()
	{
		headless = true;
		return this;
	}

	public WebDriverBuilder Incognito()
	{
		incognito = true;
		return this;
	}

	public WebDriverBuilder Maximized()
	{
		maximized = true;
		return this;
	}

	public WebDriverBuilder Minimized()
	{
		minimized = true;
		return this;
	}

	public DriverOptions Build(Browser browser)
	{
		DriverOptions options;
		switch (browser)
		{
			case Browser.Chrome:
				var chromeOptions = new ChromeOptions();
				if (headless) chromeOptions.AddArgument("--headless");
				if (incognito) chromeOptions.AddArgument("--incognito");
				if (maximized) chromeOptions.AddArgument("--start-maximized");
				if (minimized) chromeOptions.AddArgument("--start-minimized");
				options = chromeOptions;
				break;

			case Browser.Firefox:
				var firefoxOptions = new FirefoxOptions();
				if (headless) firefoxOptions.AddArgument("-headless");
				if (incognito) firefoxOptions.AddArgument("-private");
				if (maximized) firefoxOptions.AddArgument("--start-maximized");
				if (minimized) firefoxOptions.AddArgument("--start-minimized");
				options = firefoxOptions;
				break;

			case Browser.Edge:
				var edgeOptions = new EdgeOptions();
				if (headless) edgeOptions.AddArgument("--headless");
				if (incognito) edgeOptions.AddArgument("--inPrivate");
				if (maximized) edgeOptions.AddArgument("--start-maximized");
				if (minimized) edgeOptions.AddArgument("--start-minimized");
				options = edgeOptions;
				break;

			case Browser.Opera:
				var operaOptions = new ChromeOptions();
				if (headless) operaOptions.AddArgument("--headless");
				if (incognito) operaOptions.AddArgument("--incognito");
				if (maximized) operaOptions.AddArgument("--start-maximized");
				if (minimized) operaOptions.AddArgument("--start-minimized");
				options = operaOptions;
				break;

			default:
				throw new ArgumentException("Unsuported browser");
		}

		return options;
	}
}
