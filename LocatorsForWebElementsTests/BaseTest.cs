using Core.Core;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;

namespace Tests;

public abstract class BaseTest
{
	protected LoggingWebDriver driver;
	protected ILog Log
	{
		get { return LogManager.GetLogger(this.GetType()); }
	}

	[SetUp]
	public void Setup()
	{
		XmlConfigurator.Configure(new FileInfo("Log.config"));

		Browser browser = BrowserJasonParser.GetBrowserType();

		var optionsBuilder = new WebDriverBuilder();
		var options = optionsBuilder
			.Incognito()
			.DownloadReady()
			.Build(browser);

		driver = new(WebDriverFactory.GetDriver(browser, options));
	}

	[TearDown]
	public void TearDown()
	{
		if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
		{
			Log.Error("Test failed: " + TestContext.CurrentContext.Test.FullName);
			Log.Error("Error message: " + TestContext.CurrentContext.Result.Message);
		}
		else
		{
			Log.Info("Test passed: " + TestContext.CurrentContext.Test.FullName);
		}
		WebDriverFactory.QuitDriver();
	}
}
