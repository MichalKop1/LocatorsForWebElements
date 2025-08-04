using OpenQA.Selenium;

namespace Core.AltWebDriver
{
	public static class WebDriverExtensions
	{
		/// <summary>
		/// Extension method to convert the IWebDriver to LoggingWebDriver
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		public static LoggingWebDriver AsLoggingWebDriver(this IWebDriver driver)
		{
			return new LoggingWebDriver(driver);
		}
	}
}
