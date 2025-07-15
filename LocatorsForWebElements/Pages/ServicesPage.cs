using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Business.Pages;

public class ServicesPage(IWebDriver driver)
{
	private readonly IWebDriver _driver = driver;

	private static readonly By ArtificialIntelligenceButtonLocator = By.CssSelector("a[href*='artificial-intelligence'] span");
	private static readonly By GenerativeAIButtonLocator = By.CssSelector("a.button-ui-23[href*='generative-ai']");
	private static readonly By ResponsibleAIButtonLocator = By.CssSelector("a.button-ui-23[href*='responsible-ai']");
	private static readonly By ServiceTitleInside = By.CssSelector("span.museo-sans-500.gradient-text");

	public ServicesPage ClickArtificialIntelligence()
	{
		var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		var aiButton = wait.Until(drv =>
		{
			var button = drv.FindElement(ArtificialIntelligenceButtonLocator);
			return button.Displayed ? button : null;
		});

		aiButton.Click();
		return this;
	}

	public ServicesPage ClickGenerativeAi()
	{
		var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		var aiButton = wait.Until(drv =>
		{
			var button = drv.FindElement(GenerativeAIButtonLocator);
			return button.Displayed ? button : null;
		});

		aiButton.Click();
		return this;
	}

	public ServicesPage ClickResponsibleAi()
	{
		var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		var aiButton = wait.Until(drv =>
		{
			var button = drv.FindElement(ResponsibleAIButtonLocator);
			return button.Displayed ? button : null;
		});

		aiButton.Click();
		return this;
	}

	public string GetServiceTitle()
	{
		var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		var serviceTitle = wait.Until(drv =>
		{
			var title = drv.FindElement(ServiceTitleInside);
			return title.Displayed ? title.Text : null;
		});
		return serviceTitle;
	}
}
