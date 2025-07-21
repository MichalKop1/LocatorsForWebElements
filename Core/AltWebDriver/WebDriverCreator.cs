using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.AltWebDriver;

public abstract class WebDriverCreator
{
	private bool headless;
	private bool incognito;
	private bool maximized;
	private bool minimized;
	private bool downloadReady;

	public WebDriverCreator Headless()
	{
		headless = true;
		return this;
	}

	public WebDriverCreator Incognito()
	{
		incognito = true;
		return this;
	}

	public WebDriverCreator Maximized()
	{
		maximized = true;
		return this;
	}

	public WebDriverCreator Minimized()
	{
		minimized = true;
		return this;
	}

	public WebDriverCreator DownloadReady()
	{
		downloadReady = true;
		return this;
	}

	protected abstract IWebDriver CreateWebDriver();

	public IWebDriver GetConfiguredWebDriver()
	{
		IWebDriver driver = CreateWebDriver();
		return driver;
	}

	protected DriverOptions ConfigureOptions(DriverOptions options)
	{
		if (options is ChromeOptions chromeOptions)
		{
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
		}
		else if (options is FirefoxOptions firefoxOptions)
		{
			if (headless) firefoxOptions.AddArgument("-headless");
			if (incognito) firefoxOptions.AddArgument("-private");
			if (maximized) firefoxOptions.AddArgument("--start-maximized");
			if (minimized) firefoxOptions.AddArgument("--start-minimized");
		}
		else if (options is EdgeOptions edgeOptions)
		{
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
		}

		return options;
	}
}
