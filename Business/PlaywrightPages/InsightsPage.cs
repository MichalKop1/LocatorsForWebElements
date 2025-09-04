using Microsoft.Playwright;

namespace Business.PlaywrightPages;

public class InsightsPage
{
	private readonly IPage _page;

	private ILocator CarouselTextLocator => _page.Locator("//*[@id=\"main\"]/div[1]/div[1]/div/div[1]/div[1]/div/div[6]/div/div/div/div[1]/div/div/p/span");
	private ILocator CarouselText1 => _page.Locator("//span[1]");
	private ILocator CarouselText2 => _page.Locator("//span[2]");
	private ILocator CarouselText3 => _page.Locator("//span[3]");

	private ILocator CarouselRightButtonLocator => _page.Locator(".slider__right-arrow");
	private ILocator CurrentCarouselItemLocator => _page.Locator(".owl-stage").First; // fix this locator
	private ILocator CarouselReadMoreLocator => _page.Locator("[tabindex='0']", new PageLocatorOptions {HasText = "Read More" });
	private ILocator ArticleTextLocator => _page.Locator("span.font-size-80-33 > span.museo-sans-light");


	public InsightsPage(IPage page)
	{
		_page = page;
	}


	public async Task<InsightsPage> SwapCarousel(int spins = 1)
	{
		for (int  i = 0;  i < spins;  i++)
		{
			await CarouselTextLocator.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
			await CarouselRightButtonLocator.First.ClickAsync();
		}

		return this;
	}

	public async Task<string> GetCarouselText()
	{
		var textPart1 = await CarouselTextLocator.Locator(CarouselText1).First.InnerTextAsync();
		var textPart2 = await CarouselTextLocator.Locator(CarouselText2).First.InnerTextAsync();
		var textPart3 = await CarouselTextLocator.Locator(CarouselText3).InnerTextAsync();

		return textPart1 + textPart2 + textPart3;
	}

	public async Task<InsightsPage> ClickReadMore()
	{
		await CurrentCarouselItemLocator.Locator(CarouselReadMoreLocator).ClickAsync();
		return this;
	}

	public async Task<string> GetArticleText()
	{
		return await ArticleTextLocator.First.InnerTextAsync();
	}
}
