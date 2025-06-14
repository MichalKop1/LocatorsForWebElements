using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Core.Core;
using OpenQA.Selenium;

namespace Core.Core
{
	public class ScreenshotTaker
	{
		private static string NewScreenshotName
        {
            get { return "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff"); }
        }

        public static string TakeBrowserScreenshot(ITakesScreenshot driver)
        {
            var screenshotPath = Path.Combine(Environment.CurrentDirectory, "Display" + NewScreenshotName);
            Screenshot screenshot = driver.GetScreenshot();
			screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }
        //public static string TakeFullDisplayScreenshot()
        //{
        //    var screenshotPath = Path.Combine(Environment.CurrentDirectory, "FullScreen" + NewScreenshotName);
        //    using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
        //    {
        //        using (Graphics g = Graphics.FromImage(bmpScreenCapture))
        //        {
        //            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
        //                             Screen.PrimaryScreen.Bounds.Y,
        //                             0, 0,
        //                             bmpScreenCapture.Size,
        //                             CopyPixelOperation.SourceCopy);
        //        }
        //        bmpScreenCapture.Save(screenshotPath, ScreenshotImageFormat);
        //    }
        //    return screenshotPath;
        //}
	}

}
