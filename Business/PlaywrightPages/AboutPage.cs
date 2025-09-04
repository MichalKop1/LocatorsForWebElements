using Microsoft.Playwright;

namespace Business.PlaywrightPages;

public class AboutPage
{
	private IPage _page;

	private ILocator DownloadButtonLocator => _page.Locator("a.button-ui-23[download]");

	public AboutPage(IPage page)
	{
		_page = page;
	}

	public async Task<AboutPage> ScrollToDownloadButton()
	{
		await DownloadButtonLocator.ScrollIntoViewIfNeededAsync();
		return this;
	}

	public async Task<AboutPage> ClickDownloadButton(string directory, string file)
	{
		var pathToFile = Path.Combine(directory, file);
		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}
		var downloadTask = _page.WaitForDownloadAsync();
		await DownloadButtonLocator.ClickAsync();
		var download = await downloadTask;
		await download.SaveAsAsync(pathToFile);
		return this;
	}

	public static async Task CleanupDownloadedFile(string filePath)
	{
		bool fileExists = File.Exists(filePath);
		if (fileExists)
		{
			File.Delete(filePath);
		}
	}
}
