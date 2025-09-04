using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Core.AltWebDriver;
using Business.PlaywrightPages;

namespace PlaywrightTests;

[TestFixture]
public class EpamPageTests : PageTest
{
	[TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf", "https://www.epam.com/")]
	public async Task AboutPage_DownloadFile_Success(string fileName, string url)
	{
		string downloadPath = "D:\\DownloadPlaywright";
		string fullPath = Path.Combine(downloadPath, fileName);

		var indexPage = new Business.PlaywrightPages.IndexPage(Page);
		await indexPage.Open(url);
		await indexPage.AcceptCookies();

		var page2 = await indexPage
			.ClickAbout();

		var page3 = await page2.ScrollToDownloadButton();

		await page3.ClickDownloadButton(downloadPath, fileName);

		bool expected = File.Exists(fullPath);

		await AboutPage.CleanupDownloadedFile(fullPath);

		Assert.That(expected, Is.True);
	}

	[TestCase("https://www.epam.com/")]
	public async Task InsightsPage_CarouselDispalysCorrectTextInsideAndOutside(string url)
	{
		var indexPage = new Business.PlaywrightPages.IndexPage(Page);
		await indexPage.Open(url);
		await indexPage.AcceptCookies();
		var insightsPage = await indexPage
			.ClickInsights();

		await insightsPage
			.SwapCarousel(spins:2);

		string articleTextOutside = await insightsPage.GetCarouselText();

		await insightsPage
			.ClickReadMore();

		string articleTextInside = await insightsPage.GetArticleText();


		Assert.That(articleTextOutside, Is.EqualTo(articleTextInside));
	}

}
