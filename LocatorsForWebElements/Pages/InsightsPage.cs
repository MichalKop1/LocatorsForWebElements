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

	private readonly static By CarouselTextLocatorr = By.XPath("//div[contains(@class,'single-slide__content-container')]//p[contains(@class, 'scaling-of-text-wrapper')]--");
	// By.CssSelector(div  div div p span[class='font-size-60']);
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
		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

		var elementText = wait.Until(drv =>
		{
			var element = drv.FindElement(CarouselTextLocatorr);

			return element.Displayed || element.Enabled ? element : null;
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
