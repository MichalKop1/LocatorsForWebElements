using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.AltWebDriver;

public class ChromeWebDriverCreator : WebDriverCreator
{
	protected override IWebDriver CreateWebDriver()
	{
		ChromeOptions options = new ChromeOptions();
		ConfigureOptions(options);
		return new ChromeDriver(options);
	}
}
