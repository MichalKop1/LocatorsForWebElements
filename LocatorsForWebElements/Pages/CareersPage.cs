using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Pages;

public class CareersPage
{
	private readonly IWebDriver driver;

	private static readonly By KeywordSearchFieldLocator = By.Id("new_form_job_search-keyword");
	private static readonly By DropDownMenuLocator = By.ClassName("select2-selection--single");
	private static readonly By AllLocationsLiLocator = By.XPath(".//li[contains(text(), 'All Locations')]");
	private static readonly By RemoteWorkCheckBoxLocator = By.CssSelector("label[for='id-93414a92-598f-316d-b965-9eb0dfefa42d-remote']");
	private static readonly By FindButtonLocator = By.ClassName("small-button-text");
	private static readonly By AllItemsLocator = By.TagName("li");
	private static readonly By DisplayedListLocator = By.ClassName("search-result__list");
	private static readonly By AltButtonApplyLocator = By.PartialLinkText("VIEW");
	private static readonly By LanguageNameLocator = By.TagName("article");
	private static readonly By JobListingTextLocator = By.Id("main");
	private static readonly By ErrorMessageLocator = By.ClassName("search-result__error-message-23");

	public CareersPage(IWebDriver driver)
	{
		this.driver = driver;
	}

	public CareersPage Open()
	{
		return this;
	}

	public CareersPage FillInSearchInfo(string codingLanguage)
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		EnterKeyword(codingLanguage);
		SelectAllLocations(wait);
		SelectRemoteWork();

		return this;
	}


	private void EnterKeyword(string codingLanguage)
	{
		var keywordSearchField = driver.FindElement(KeywordSearchFieldLocator);
		keywordSearchField.Click();
		keywordSearchField.SendKeys(codingLanguage);
	}

	private void SelectAllLocations(WebDriverWait wait)
	{
		var dropDownMenuClick = driver.FindElement(DropDownMenuLocator);
		dropDownMenuClick.Click();
		var allLi = wait.Until(d => d.FindElement(AllLocationsLiLocator));
		allLi.Click();
	}

	private void SelectRemoteWork()
	{
		var remoteWorkCheckBox = driver.FindElement(RemoteWorkCheckBoxLocator);
		remoteWorkCheckBox.Click();
	}

	public CareersPage ClickFindButton()
	{
		WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		var findButton = wait.Until(d => d.FindElement(FindButtonLocator));
		findButton.Click();

		return this;
	}

	public bool WaitForNoResults()
	{
		WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		var result = wait.Until(driver =>
			driver.FindElements(ErrorMessageLocator)
				.Any(e => !string.IsNullOrEmpty(e.Text)));

		return result;
	}

	public CareersPage WaitForExpectedResults()
	{
		WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		wait.Until(driver =>
		{
			var resultsList = driver.FindElements(DisplayedListLocator).FirstOrDefault();
			if (resultsList != null)
			{
				var items = resultsList.FindElements(AllItemsLocator);
				return items.Count > 0;
			}
			return false;
		});

		return this;
	}

	public CareersPage OpenFirstResult()
	{
		WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		var altbuttonApply = wait.Until(d =>
		{
			var element = d.FindElement(AltButtonApplyLocator);
			return element.Enabled ? element : null;
		});
		altbuttonApply.Click();
		driver.Navigate().Refresh();
		return this;
	}

	public bool IsLanguageInResult(string codingLanguage)
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		wait.Until(drv =>
		{
			var lang = drv.FindElement(LanguageNameLocator);

			return lang.Displayed;
		});
		driver.Manage().Window.Maximize();

		var domString = driver.FindElement(JobListingTextLocator).Text;

		return domString.Contains(codingLanguage);
	}
}
