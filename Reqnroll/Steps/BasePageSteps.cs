using Core.Core;
using log4net;
using log4net.Config;
using Reqnroll;

namespace SpecFlowTests.Steps
{
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

			Browser browser = BrowserJasonParser.GetBrowserType();

			var optionsBuilder = new WebDriverBuilder();
			var options = optionsBuilder
				.Incognito()
				.Build(browser);

			driver = new(WebDriverFactory.GetDriver(browser, options));
		}

		[AfterScenario]
		public void TearDown()
		{
			WebDriverFactory.QuitDriver();
		}
	}
}
