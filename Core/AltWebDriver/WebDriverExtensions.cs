using Core.Core;
using OpenQA.Selenium;

namespace Core.AltWebDriver
{
	public static class WebDriverExtensions
	{
		public static LoggingWebDriver AsLoggingWebDriver(this IWebDriver driver)
		{
			return new LoggingWebDriver(driver);
		}
	}
}
