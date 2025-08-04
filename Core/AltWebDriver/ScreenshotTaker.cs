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
			var screenshotsDir = FindTestProjectRoot();
			if (!Directory.Exists(screenshotsDir))
			{
				Directory.CreateDirectory(screenshotsDir);
			}
			var screenshotPath = Path.Combine(screenshotsDir, "Display" + NewScreenshotName + ".png");
			Screenshot screenshot = driver.GetScreenshot();
			screenshot.SaveAsFile(screenshotPath);
			return screenshotPath;
		}

		private static string FindTestProjectRoot(string markerFolderName = "Screenshots")
		{
			string dir = AppContext.BaseDirectory;

			while (dir != null)
			{
				string potentialPath = Path.Combine(dir, markerFolderName);
				if (Directory.Exists(potentialPath))
					return potentialPath;

				dir = Directory.GetParent(dir)?.FullName;
			}

			throw new DirectoryNotFoundException($"Could not locate '{markerFolderName}' folder in parent directories.");
		}
	}
}
