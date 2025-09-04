using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests;

[SetUpFixture]
public class LoginFixture
{
	public IPlaywright _playwright { get; set; }
	public IBrowser _browser { get; set; }

	[OneTimeSetUp]
	public async Task Login()
	{
		_playwright = await Playwright.CreateAsync();
		_browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {Headless = false });
		var context = await _browser.NewContextAsync();
		var page = await context.NewPageAsync();

		await page.GotoAsync("https://commitquality.com/login");
		await page.GetByTestId("username-textbox").FillAsync("test");
		await page.GetByTestId("password-textbox").FillAsync("test");
		await page.GetByTestId("login-button").ClickAsync();

		await context.StorageStateAsync(new BrowserContextStorageStateOptions
		{
			Path = "./../../../state.json"
		});

		await page.CloseAsync();
		await context.CloseAsync();
	}

}
