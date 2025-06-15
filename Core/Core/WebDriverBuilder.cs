using Core.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Core;

public class WebDriverBuilder
{
	private bool headless;
	private bool incognito;
	private bool maximized;
	private bool minimized;
	private bool downloadReady;

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

	public WebDriverBuilder DownloadReady()
	{
		downloadReady = true;
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
				if (downloadReady)
				{
					chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
					chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
					chromeOptions.AddUserProfilePreference("safebrowsing.enabled", false);
					chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
					chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
				}
				
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
				if (downloadReady)
				{
					edgeOptions.AddUserProfilePreference("download.prompt_for_download", false);
					edgeOptions.AddUserProfilePreference("download.directory_upgrade", true);
					edgeOptions.AddUserProfilePreference("safebrowsing.enabled", false);
					edgeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
					edgeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
				}
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
