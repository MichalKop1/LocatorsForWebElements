using OpenQA.Selenium;
using LocatorsForWebElements.Pages;
using OpenQA.Selenium.Chrome;
using LocatorsForWebElements.Helpers;
using LocatorsForWebElements.Factories;

namespace LocatorsForWebElementsTests;

[TestFixture(Browser.Edge)]
public class EpamPageTests(Browser browser)
{
	private IWebDriver driver;

	[SetUp]
	public void Setup()
	{
		var optionsBuilder = new WebDriverBuilder();
		var options = optionsBuilder
			.Incognito()
			.DownloadReady()
			.Build(browser);

		driver = WebDriverFactory.GetDriver(browser, options);
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
			.ClickDownloadButton();

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
		WebDriverFactory.QuitDriver();
	}

}
