using OpenQA.Selenium;
using LocatorsForWebElements.Pages;
using OpenQA.Selenium.Chrome;
using LocatorsForWebElements.Helpers;
using LocatorsForWebElements.Factories;

namespace LocatorsForWebElementsTests;

[TestFixture]
public class EpamPageTests
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
			.Build(Browser.Chrome);

		driver = WebDriverFactory.GetDriver(Browser.Chrome, options);
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
			.OpenFirstResult();

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

	[Test]
	[TestCase("BLOCKCHAIN", true)]
	[TestCase("Automation", false)]
	[TestCase("Cloud", true)]
	public void FindLinks_AllElementsContainPhrase(string phrase, bool expected)
	{
		indexPage = new IndexPage(driver);
		indexPage.Open().AcceptCookies();

		//searchPage = indexPage.Search(phrase);
		searchPage = indexPage
			.ClickSearchIcon()
			.WaitForSearchPanel()
			.EnterSearchPhrase(phrase)
			.ClickFindButton()
			.NavigateToSearchPage();

		bool areLinksFound = searchPage.FindLinks(phrase.ToLower());

		Assert.That(areLinksFound, Is.EqualTo(expected));
	}

	[TearDown]
	public void TearDown()
	{
		WebDriverFactory.QuitDriver();
	}

}
