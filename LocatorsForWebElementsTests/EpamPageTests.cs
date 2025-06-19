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
	private IndexPage indexPage;
	private CareersPage careersPage;
	private SearchPage searchPage;

	[SetUp]
	public void Setup()
	{
		var optionsBuilder = new WebDriverBuilder();
		var options = optionsBuilder
			.Incognito()
			.Build(browser);

		driver = WebDriverFactory.GetDriver(browser, options);
		indexPage = new IndexPage(driver);
	}

	[TestCase("C#", true)]
	[TestCase("Python", true)]
	[TestCase("Java", true)]
	public void FindMatchingLanguageJob_LanguageExists(string language, bool expected)
	{
		indexPage.Open().AcceptCookies();

		careersPage = indexPage.SelectCareers();

		var fillInThePage = careersPage.FillInSearchInfo(language).ClickFindButton();
		var openFirstListing = fillInThePage
			.WaitForExpectedResults()
			.OpenLatestResult();

		bool actual = openFirstListing.IsLanguageInResult(language);

		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("C++", true)]
	[TestCase("Java Script", true)]
	public void FindMatchingLanguageJob_LanguageDoesntExists(string language, bool expected)
	{
		indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		careersPage = indexPage
			.SelectCareers()
			.FillInSearchInfo(language)
			.ClickFindButton();

		bool actual = careersPage.WaitForNoResults();

		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("BLOCKCHAIN", "Cloud", true)]
	public void FindLinks_AllElementsContainPhrase(string phrase1, string phrase2, bool expected)
	{
		indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		searchPage = indexPage
			.ClickSearchIcon()
			.WaitForSearchPanel()
			.EnterSearchPhrase(phrase1)
			.ClickFindButton()
			.NavigateToSearchPage();

		bool areLinksFound = searchPage.FindLinks(phrase1.ToLower());
		Assert.That(areLinksFound, Is.EqualTo(expected));

		searchPage = searchPage.GoBack()
			.ClickSearchIcon()
			.WaitForSearchPanel()
			.EnterSearchPhrase(phrase2)
			.ClickFindButton()
			.NavigateToSearchPage();

		areLinksFound = searchPage.FindLinks(phrase2.ToLower());
		Assert.That(areLinksFound, Is.EqualTo(expected));
	}

	[TestCase("Automation", false)]
	public void FindLinks_NotAllElementsContainPhrase(string phrase1, bool expected)
	{
		indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		searchPage = indexPage
			.ClickSearchIcon()
			.WaitForSearchPanel()
			.EnterSearchPhrase(phrase1)
			.ClickFindButton()
			.NavigateToSearchPage();

		bool areLinksFound = searchPage.FindLinks(phrase1.ToLower());

		Assert.That(areLinksFound, Is.EqualTo(expected));
	}

	[TearDown]
	public void TearDown()
	{
		WebDriverFactory.QuitDriver();
	}

}
