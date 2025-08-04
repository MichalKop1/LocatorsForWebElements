using OpenQA.Selenium;

namespace Core.AltWebDriver
{
	public static class ScreenshotTaker
	{
		private static string NewScreenshotName
		{
			get { return "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff"); }
		}

		public static string TakeBrowserScreenshot(ITakesScreenshot driver)
		{
			var screenshotsDir = Path.Combine(Environment.CurrentDirectory, "Screenshots");
			if (!Directory.Exists(screenshotsDir))
			{
				Directory.CreateDirectory(screenshotsDir);
			}
			var screenshotPath = Path.Combine(screenshotsDir, "Display" + NewScreenshotName + ".png");
			Screenshot screenshot = driver.GetScreenshot();
			screenshot.SaveAsFile(screenshotPath);
			return screenshotPath;
		}
	}

}
