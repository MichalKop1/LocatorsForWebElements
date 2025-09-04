using Microsoft.Playwright;
using OpenQA.Selenium;

namespace Business.PlaywrightPages;

public class CareersPage
{
	private IPage _page;

	private ILocator KeywordSearchFieldLocator => _page.Locator("#new_form_job_search-keyword");
	private ILocator DropDownMenuLocator => _page.Locator(".select2-selection--single");
	private ILocator AllLocationsLiLocator => _page.Locator(".//li[contains(text(), 'All Locations')]\"");
	private ILocator RemoteWorkCheckBoxLocator => _page.Locator("fieldset div p[class='job-search__filter-items job-search__filter-items--remote'] label");
	private ILocator FindButtonLocator => _page.Locator(".small-button-text");
	private ILocator AllItemsLocator => _page.Locator("li");
	private ILocator DisplayedListLocator  => _page.Locator(".search-result__list");
	private ILocator AltButtonApplyLocator  => _page.GetByText("VIEW");
	private ILocator LanguageNameLocator => _page.Locator("article");
	private ILocator ErrorMessageLocator => _page.Locator(".search-result__error-message-23");

	public CareersPage(IPage page)
	{
		_page = page;
	}

	public async Task<CareersPage> Open(string url)
	{
		await _page.GotoAsync(url);
		return this;
	}

	public async Task<CareersPage> FillInSearchInfo(string codingLanguage)
	{
		await EnterKeyword(codingLanguage);
		await SelectAllLocations();
		await SelectRemoteWork();
		return this;
	}

	private async Task EnterKeyword(string codingLanguage)
	{
		await KeywordSearchFieldLocator.FillAsync(codingLanguage);
	}

	private async Task SelectAllLocations()
	{
		await DropDownMenuLocator.ClickAsync();
		await AllLocationsLiLocator.ClickAsync();
	}

	private async Task SelectRemoteWork()
	{
		await RemoteWorkCheckBoxLocator.ClickAsync();
	}

	public async Task<CareersPage> ClickFindButton()
	{
		await FindButtonLocator.ClickAsync();
		return this;
	}

	public async Task<bool> WaitForNoResults()
	{
		var result = ErrorMessageLocator.IsVisibleAsync();
		return await result;
	}

	public async Task<CareersPage> WaitForExpectedResults()
	{
		await DisplayedListLocator.Locator(AllItemsLocator).IsVisibleAsync();
		return this;
	}

	public async Task<CareersPage> OpenFirstResult()
	{
		await AltButtonApplyLocator.ClickAsync();
		return this;
	}

	public async Task<bool> IsLanguageInResult(string codingLanguage)
	{
		var domString = LanguageNameLocator.First.InnerTextAsync();
		return (await domString).Contains(codingLanguage);
	}
}
