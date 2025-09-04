
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests;

public class PlaywrightTestBase
{
	public IBrowser Browser { get; set; }
	protected IPage Page { get; set; }

	[SetUp]
	public async Task Setup()
	{
		var playwright = await Playwright.CreateAsync();
		Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
		{
			Headless = false
		});
		var context = await Browser.NewContextAsync(new BrowserNewContextOptions
		{
			StorageStatePath = "./../../../state.json"
		});
		Page = await context.NewPageAsync();
	}

	[TearDown]
	public async Task Teardown()
	{
		await Browser.CloseAsync();
		await Page.CloseAsync();
	}
}
