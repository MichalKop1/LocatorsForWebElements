using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.Pages;

public class SearchPage
{
	private readonly IWebDriver driver;

	private static readonly By ItemListLocator = By.ClassName("search-results__items");
	public SearchPage(IWebDriver driver)
	{
		this.driver = driver;
	}

	public bool FindLinks(string phrase)
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		var itemList = wait.Until(driver =>
		{
			var elements = driver.FindElements(ItemListLocator);
			return elements.Count != 0 ? elements : null;
		});

		return itemList.All(item => item.Text.ToLower().Contains(phrase));
	}

	public IndexPage GoBack()
	{
		driver.Navigate().Back();

		return new IndexPage(driver);
	}
}
