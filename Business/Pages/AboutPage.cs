using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Pages;

public class AboutPage
{
	private readonly IWebDriver driver;

	private static readonly By DownloadButtonLocator = By.CssSelector("a.button-ui-23[download]");

	public AboutPage(IWebDriver driver)
	{
		this.driver = driver ?? throw new ArgumentException(nameof(driver));
	}

	public AboutPage ScrollToDownloadButton()
	{
		var downloadButton = driver.FindElement(DownloadButtonLocator);
		
		Actions actions = new Actions(driver);
		actions
			.MoveToElement(downloadButton)
			.Perform();

		return this;
	}

	public AboutPage ClickDownloadButton(string directory, string file)
	{
		var pathToFile = Path.Combine(directory, file);

		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
		var downloadButton = wait.Until(drv =>
		{
			var element = drv.FindElement(DownloadButtonLocator);
			return element.Displayed ? element : null;
		});

		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		downloadButton.Click();
		wait.Until(_ => File.Exists(pathToFile));

		return this;
	}
}
