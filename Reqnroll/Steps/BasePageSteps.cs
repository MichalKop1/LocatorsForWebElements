using Core.AltWebDriver;
using log4net;
using log4net.Config;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace SpecFlowTests.Steps;

[Binding]
public abstract class BasePageSteps
{
	protected LoggingWebDriver driver;

	protected ILog Log
	{
		get { return LogManager.GetLogger(this.GetType()); }
	}

	[BeforeScenario]
	public void Setup()
	{
		XmlConfigurator.Configure(new FileInfo("Log.config"));

		WebDriverCreator create = WebDriverCreatorFactory.GetCreator();
		driver = create
			.Incognito()
			.DownloadReady()
			.Maximized()
			.GetConfiguredWebDriver()
			.AsLoggingWebDriver(); // extension method to convert the IWebDriver to LoggingWebDriver
	}

	[AfterScenario]
	public void TearDown()
	{
		driver.Quit();
	}
}
