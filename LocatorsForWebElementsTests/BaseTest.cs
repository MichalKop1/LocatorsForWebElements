using Core.AltWebDriver;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;

[assembly: XmlConfigurator(ConfigFile = "Common/Log.config", Watch = true)]

namespace WebDriverTests;

public abstract class BaseTest
{
	protected LoggingWebDriver driver;
	protected ILog Log
	{
		get { return LogManager.GetLogger(GetType()); }
	}

	[SetUp]
	public void Setup()
	{
		WebDriverCreator create = WebDriverCreatorFactory.GetCreator();
		driver = create
			.Headless()
			.Maximized()
			.DownloadReady()
			.GetConfiguredWebDriver()
			.AsLoggingWebDriver();
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
		driver.Dispose();
		driver.Driver.Quit();
	}
}
