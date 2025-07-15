using Core.Enums;
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
		return browser switch
		{
			Browser.Chrome => ConfigureChromeOptions(),
			Browser.Firefox => ConfigureFirefoxOptions(),
			Browser.Edge => ConfigureEdgeOptions(),
			Browser.Opera => ConfigureOperaOptions(),
			_ => throw new ArgumentException("Unsupported browser")
		};
	}

	private ChromeOptions ConfigureChromeOptions()
	{
		var options = new ChromeOptions();
		ApplyCommonOptions(options);
		return options;
	}

	private FirefoxOptions ConfigureFirefoxOptions()
	{
		var options = new FirefoxOptions();
		if (headless) options.AddArgument("-headless");
		if (incognito) options.AddArgument("-private");
		if (maximized) options.AddArgument("--start-maximized");
		if (minimized) options.AddArgument("--start-minimized");
		return options;
	}

	private EdgeOptions ConfigureEdgeOptions()
	{
		var options = new EdgeOptions();
		ApplyCommonOptions(options);
		return options;
	}

	private ChromeOptions ConfigureOperaOptions()
	{
		var options = new ChromeOptions();
		ApplyCommonOptions(options);
		return options;
	}

	private void ApplyCommonOptions(dynamic options)
	{
		if (headless) options.AddArgument("--headless");
		if (incognito) options.AddArgument(options is EdgeOptions ? "--inPrivate" : "--incognito");
		if (maximized) options.AddArgument("--start-maximized");
		if (minimized) options.AddArgument("--start-minimized");
		if (downloadReady)
		{
			options.AddUserProfilePreference("download.prompt_for_download", false);
			options.AddUserProfilePreference("download.directory_upgrade", true);
			options.AddUserProfilePreference("safebrowsing.enabled", false);
			options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
			options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
		}
	}
}

