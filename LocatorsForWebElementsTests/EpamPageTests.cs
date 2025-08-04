using OpenQA.Selenium;
using Business.Pages;
using Core.AltWebDriver;
using WebDriverTests.Common;

namespace WebDriverTests;

[TestFixture]
public class EpamPageTests : BaseTest
{
	[Log]
	[TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
	public void AboutPage_DownloadFile_Success(string fileName)
	{
		string downloadsPath = JsonStringParser.GetConfig().DownloadPath;
		string fullPath = Path.Combine(downloadsPath, fileName);

		var indexPage = new IndexPage(driver);
		ScreenshotTaker.TakeBrowserScreenshot((ITakesScreenshot)driver.Driver);
		indexPage.Open().AcceptCookies();

		indexPage
			.SelectAbout()
			.ScrollToDownloadButton()
			.ClickDownloadButton(downloadsPath, fileName);
		ScreenshotTaker.TakeBrowserScreenshot((ITakesScreenshot)driver.Driver);
		bool fileExists = File.Exists(fullPath);
		if (fileExists)
		{
			File.Delete(fullPath);
		}

		Assert.That(fileExists, Is.True);
	}

	[Log]
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
		ScreenshotTaker.TakeBrowserScreenshot((ITakesScreenshot)driver.Driver);

		Assert.That(articleTextOutside, Is.Not.EqualTo(articleTextInside));
	}
}
