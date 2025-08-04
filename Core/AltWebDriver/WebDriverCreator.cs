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

	/// <summary>
	/// Add this option to configure the driver to be ready for downloading files without pop-up windows.
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public WebDriverCreator DownloadReady()
	{
		downloadReady = true;
		return this;
	}

	/// <summary>
	/// Left to be implemented in derived classes to specify the driver type.
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	protected abstract IWebDriver CreateWebDriver();

	/// <summary>
	/// Returns a configured WebDriver instance based on the options set in this creator.
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public IWebDriver GetConfiguredWebDriver()
	{
		log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.config"));
		IWebDriver driver = CreateWebDriver();
		return driver;
	}

	protected DriverOptions ConfigureOptions(DriverOptions options)
	{
		if (options is ChromeOptions chromeOptions)
		{
			chromeOptions.AddArgument("--window-size=2600,1800");
			if (headless)
			{
				chromeOptions.AddArgument("--headless=new");
				chromeOptions.AddArgument("--disable-gpu");
			}
			if (incognito) chromeOptions.AddArgument("--incognito");
			if (maximized) chromeOptions.AddArgument("--start-maximized");
			if (minimized) chromeOptions.AddArgument("--start-minimized");

			chromeOptions.AddArgument("--profile-directory=Default");

			chromeOptions.AddArgument("--no-first-run");
			chromeOptions.AddArgument("--no-default-browser-check");

			chromeOptions.AddArgument("--ignore-certificate-errors");
			chromeOptions.AddArgument("--allow-running-insecure-content");
			chromeOptions.AddArgument("--disable-extensions");
			chromeOptions.AddArgument("--proxy-server='direct://'");
			chromeOptions.AddArgument("--proxy-bypass-list=*");
			chromeOptions.AddArgument("--no-sandbox");
			chromeOptions.AddArgument("--disable-dev-shm-usage");

			chromeOptions.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36");
			chromeOptions.AddArgument("--lang=en-US,en;q=0.9");
			chromeOptions.AddArgument("--disable-3d-apis");
			chromeOptions.AddArgument("--disable-webgl");
			chromeOptions.AddArgument("--disable-features=IsolateOrigins,site-per-process");

			chromeOptions.AddExcludedArgument("enable-automation");
			chromeOptions.AddAdditionalOption("useAutomationExtension", false);
			chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");

			if (downloadReady)
			{
				chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
				chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
				chromeOptions.AddUserProfilePreference("safebrowsing.enabled", false);
				chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
				chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
				chromeOptions.AddUserProfilePreference("download.default_directory", "C:\\Downloads");
			}

			chromeOptions.AddArgument("--disable-infobars");
			chromeOptions.AddArgument("--remote-allow-origins=*");
			chromeOptions.AddArgument("--disable-notifications");
			chromeOptions.AddArgument("--disable-popup-blocking");
			chromeOptions.AddArgument("--disable-default-apps");
			chromeOptions.AddArgument("--test-type");
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
			edgeOptions.AddArgument("--window-size=2600,1800");
			if (headless)
			{
				edgeOptions.AddArgument("--headless=new");
				edgeOptions.AddArgument("--disable-gpu");
				edgeOptions.AddArgument("--disable-features=msUndersideButton,msEdgeSidebar,PwaSkipServiceWorker");
			}
			if (incognito) edgeOptions.AddArgument("--inprivate");
			if (maximized) edgeOptions.AddArgument("--start-maximized");
			if (minimized) edgeOptions.AddArgument("--start-minimized");

			// Security/Proxy settings
			edgeOptions.AddArgument("--ignore-certificate-errors");
			edgeOptions.AddArgument("--allow-running-insecure-content");
			edgeOptions.AddArgument("--disable-extensions");
			edgeOptions.AddArgument("--proxy-server='direct://'");
			edgeOptions.AddArgument("--proxy-bypass-list=*");
			edgeOptions.AddArgument("--no-sandbox");
			edgeOptions.AddArgument("--disable-dev-shm-usage");

			edgeOptions.AddExcludedArgument("enable-automation");
			edgeOptions.AddAdditionalOption("useAutomationExtension", false);
			edgeOptions.AddArgument("--disable-blink-features=AutomationControlled");
			edgeOptions.AddArgument("--disable-infobars");
			edgeOptions.AddArgument("--disable-web-security");
			edgeOptions.AddArgument("--disable-notifications");
			edgeOptions.AddArgument("--remote-allow-origins=*");

			if (downloadReady)
			{
				edgeOptions.AddUserProfilePreference("download.prompt_for_download", false);
				edgeOptions.AddUserProfilePreference("download.directory_upgrade", true);
				edgeOptions.AddUserProfilePreference("safebrowsing.enabled", false);
				edgeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
				edgeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
				edgeOptions.AddUserProfilePreference("download.default_directory", "C:\\Downloads");
			}

			edgeOptions.AddArgument("--disable-features=msEdgeShopping,msEdgeNews");
			edgeOptions.AddArgument("--disable-component-update");
		}

		return options;
	}
}
