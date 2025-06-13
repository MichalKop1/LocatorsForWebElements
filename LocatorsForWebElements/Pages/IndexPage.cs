using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace LocatorsForWebElements.Pages;

public class IndexPage
{
	private static string Url { get; } = "https://www.epam.com";

	private readonly IWebDriver driver;

	private static readonly By AcceptCookiesLocator = By.Id("onetrust-accept-btn-handler");
	private static readonly By CareerButtonLocator = By.LinkText("Careers");
	private static readonly By SearchIconLocator = By.ClassName("search-icon");
	private static readonly By SearchPanelLocator = By.XPath("//div[contains(@class, 'header-search__panel') and contains(@style, 'display: block')]");
	private static readonly By SearchInputLocator = By.Name("q");
	private static readonly By FindButtonLocator = By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button");

	public IndexPage(IWebDriver driver)
	{
		this.driver = driver ?? throw new ArgumentException(nameof(driver));
	}

	public IndexPage Open()
	{
		driver.Url = Url;
		return this;
	}

	public IndexPage AcceptCookies()
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException));

		wait.Until(drv =>
		{
			var cookies = drv.FindElement(AcceptCookiesLocator);
			if (cookies.Displayed && cookies.Enabled)
			{
				cookies.Click();
				return true;
			}

			return false;
		});

		return this;
	}

	public CareersPage SelectCareers()
	{
		var careerButton = driver.FindElement(CareerButtonLocator);

		careerButton.Click();

		return new CareersPage(driver);
	}

	public IndexPage ClickSearchIcon()
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		var searchIcon = wait.Until(drv => drv.FindElement(SearchIconLocator));

		var actions = new Actions(driver);
		actions.Pause(TimeSpan.FromSeconds(1))
			.Click(searchIcon)
			.Perform();

		return this;
	}

	public IndexPage WaitForSearchPanel()
	{
		var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

		searchPanelWait.Until(driver => driver.FindElement(SearchPanelLocator));
		return this;
	}

	public IndexPage EnterSearchPhrase(string phrase)
	{
		var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
		
		var searchPanel = searchPanelWait.Until(driver => driver.FindElement(SearchPanelLocator));
		var searchInput = searchPanelWait.Until(_ => searchPanel.FindElement(SearchInputLocator));

		var actions = new Actions(driver);
		actions.Click(searchInput)
			.Pause(TimeSpan.FromSeconds(1))
			.SendKeys(phrase)
			.Perform();

		return this;
	}

	public IndexPage ClickFindButton()
	{
		var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

		var searchPanel = searchPanelWait.Until(driver => driver.FindElement(SearchPanelLocator));
		var findButton = searchPanel.FindElement(FindButtonLocator);
		findButton.Click();

		return this;
	}

	public SearchPage NavigateToSearchPage()
	{
		return new SearchPage(driver);
	}
}
