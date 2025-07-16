using Core.Core;
using OpenQA.Selenium;
using Tests;
using Business.Pages;

namespace LocatorsForWebElementsTests;

[TestFixture]
public class EpamPageTests : BaseTest
{
	[TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
	public void AboutPage_DownloadFile_Success(string fileName)
	{
		string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
		string fullPath = Path.Combine(downloadsPath, fileName);

		var indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		indexPage
			.SelectAbout()
			.ScrollToDownloadButton()
			.ClickDownloadButton(fullPath);

		bool fileExists = File.Exists(fullPath);
		if (fileExists)
		{
			File.Delete(fullPath);
		}

		Assert.That(fileExists, Is.True);
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
		ScreenshotTaker.TakeBrowserScreenshot((ITakesScreenshot)driver.Driver);

		Assert.That(articleTextOutside, Is.EqualTo(articleTextInside));
	}
}
