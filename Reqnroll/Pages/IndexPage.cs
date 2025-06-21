using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowTests.Pages;

public class IndexPage(IWebDriver driver)
{
	private readonly IWebDriver _driver = driver;

	private static readonly By AcceptCookiesLocator = By.Id("onetrust-accept-btn-handler");
	private static readonly By ServicesButtonLocator = By.LinkText("Services");

	public IndexPage Open(string link)
	{
		_driver.Url = link;
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

	public ServicesPage SelectServices()
	{
		var careerButton = driver.FindElement(ServicesButtonLocator);

		careerButton.Click();

		return new ServicesPage(driver);
	}

}
