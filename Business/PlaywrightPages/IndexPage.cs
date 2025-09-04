using Microsoft.Playwright;

namespace Business.PlaywrightPages;

public class IndexPage
{
	public readonly IPage _page;

	private ILocator AcceptCookiesLocator => _page.Locator("#onetrust-accept-btn-handler");
	private ILocator CareerButtonLocator => _page.Locator("ul.top-navigation__row a[href='/careers']", new PageLocatorOptions { HasText = "Careers" });
	private ILocator AboutButtonLocator => _page.Locator("ul.top-navigation__row a[href='/about']", new PageLocatorOptions {HasText="About" });
	private ILocator InsightsButtonLocator => _page.Locator("ul.top-navigation__row a[href='/insights']", new PageLocatorOptions { HasText = "Insights" });
	private ILocator SearchIconLocator => _page.Locator(".search-icon");
	private ILocator SearchPanelLocator => _page.Locator("//div[contains(@class, 'header-search__panel') and contains(@style, 'display: block')]");
	private ILocator SearchInputLocator => _page.Locator("#new_form_search");
	private ILocator FindButtonLocator => _page.Locator(".//*[@class='search-results__input-holder']/following-sibling::button");
	private ILocator ServicesButtonLocator => _page.Locator("ul.top-navigation__row a[href='/services']", new PageLocatorOptions { HasText = "Services" });


	public IndexPage(IPage page)
	{
		_page = page;
	}

	public async Task<IndexPage> Open(string url)
	{
		await _page.GotoAsync(url);
		return this;
	}

	public async Task<IndexPage> AcceptCookies()
	{
		await AcceptCookiesLocator.ClickAsync(new LocatorClickOptions 
		{
			Timeout = 3000 
		});
		return this;
	}

	public async Task<IndexPage> ClickCareer()
	{
		await CareerButtonLocator.ClickAsync();
		return this;
	}

	public async Task<IndexPage> ClickServices()
	{
		await ServicesButtonLocator.ClickAsync();
		return this;
	}

	public async Task<InsightsPage> ClickInsights()
	{
		await InsightsButtonLocator.First.ClickAsync();
		return new InsightsPage(_page);
	}

	public async Task<AboutPage> ClickAbout()
	{
		await AboutButtonLocator.First.ClickAsync();
		return new AboutPage(_page);
	}

	public async Task<IndexPage> ClickSearchIcon()
	{
		await SearchIconLocator.ClickAsync();
		return this;
	}

	public async Task<IndexPage> EnterSearchPhrase(string text)
	{
		await SearchInputLocator.FillAsync(text);
		return this;
	}

	public async Task<IndexPage> ClickFindButton()
	{
		await SearchPanelLocator.Locator(FindButtonLocator).ClickAsync();
		return this;
	}
}
