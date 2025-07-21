using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Core.AltWebDriver;

public class FirefoxWebDriverCreator : WebDriverCreator
{
	protected override IWebDriver CreateWebDriver()
	{
		FirefoxOptions options = new FirefoxOptions();
		ConfigureOptions(options);
		return new FirefoxDriver(options);
	}
}
