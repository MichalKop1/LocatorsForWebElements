using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.AltWebDriver;

public class EdgeWebDriverCreator : WebDriverCreator
{
	protected override IWebDriver CreateWebDriver()
	{
		EdgeOptions options = new EdgeOptions();
		ConfigureOptions(options);
		return new EdgeDriver(options);
	}
}
