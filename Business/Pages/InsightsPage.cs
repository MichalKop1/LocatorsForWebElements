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

public class InsightsPage
{
	private readonly IWebDriver driver;

	private readonly static By CarouselTextLocator = By.XPath("//*[@id=\"main\"]/div[1]/div[1]/div/div[1]/div[1]/div/div[6]/div/div/div/div[1]/div[2]/div/p/span");
	private readonly static By CarouselRightButtonLocator = By.ClassName("slider__right-arrow");
	private readonly static By CarouselReadMoreLocator = By.PartialLinkText("Read More");
	private readonly static By ArticleTextLocator = By.CssSelector("span.font-size-80-33 > span.museo-sans-light");

	public InsightsPage(IWebDriver driver)
	{
		this.driver = driver ?? throw new ArgumentException(nameof(driver));
	}

	public InsightsPage SwapCarousel()
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
		var rightCarouselRight = wait.Until(drv => drv.FindElement(CarouselRightButtonLocator));
		rightCarouselRight.Click();

		return this;
	}

	public string GetCarouselText()
	{
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

		var elementText = wait.Until(drv =>
		{
			var element = drv.FindElement(CarouselTextLocator);

			return element.Displayed ? element : null;
		});

		return elementText.Text;
	}

	public InsightsPage ClickReadMore()
	{
		var readMoreButton = driver.FindElement(CarouselReadMoreLocator);
		readMoreButton.Click();

		return this;
	}

	public string GetArticleText()
	{
		var article = driver.FindElement(ArticleTextLocator);
		return article.Text;
	}
}
