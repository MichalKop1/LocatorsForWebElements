using Core.Core;
using Business.Pages;
using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

namespace LocatorsForWebElementsTests;

[TestFixture(Browser.Edge)]
public class EpamPageTests(Browser browser)
{
	private LoggingWebDriver driver;

	protected ILog Log
	{
		get { return LogManager.GetLogger(this.GetType()); }
	}

	[SetUp]
	public void Setup()
	{
		XmlConfigurator.Configure(new FileInfo("Log.config"));

		var optionsBuilder = new WebDriverBuilder();
		var options = optionsBuilder
			.Incognito()
			.DownloadReady()
			.Build(browser);

		driver = new(WebDriverFactory.GetDriver(browser, options));
	}

	[TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
	public void AboutPage_DownloadFile_Success(string fileName)
	{
		string pathToDesktop = "F:\\downloader";
		string fullPath = Path.Combine(pathToDesktop, fileName);

		var indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		indexPage
			.SelectAbout()
			.ScrollToDownloadButton()
			.ClickDownloadButton(fullPath);

		Assert.That(File.Exists(fullPath), Is.True);
	}
	
	[Test]
	public void InsightsPage_CarouselDispalysCorrectTextInsideAndOutside()
	{
		var indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		var insightsPage = indexPage
			.SelectInsights()
			.SwapCarousel()
			.SwapCarousel();

		string articleTextOutside = insightsPage.GetCarouselText();

		string articleTextInside = insightsPage
			.ClickReadMore()
			.GetArticleText();

		Assert.That(articleTextOutside, Is.EqualTo(articleTextInside));
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
