using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PlaywrightTests;

[TestFixture]
public class Tests : PlaywrightTestBase
{
	// dotnet test --settings chromeSettings.runsettings 
	// to run script using settings file
	private IBrowser browser;
	private IBrowserContext context;
	//private IPage page;

	[SetUp]
	public async Task Setup()
	{
		

		var playwright = Playwright.CreateAsync().Result;
		var LaunchOptions = new BrowserTypeLaunchOptions
		{
			Headless = false
		};
		browser = await playwright.Chromium.LaunchAsync(LaunchOptions);
		context = await browser.NewContextAsync();

	}


	[Test]
	public async Task Check_if_Exists_Career()
	{
		var environmentalVariable = Environment.GetEnvironmentVariable("baseUrl");
		var testVariable = TestContext.Parameters["Browser"];


		await Page.GotoAsync("https://commitquality.com/");
		//await Expect(Page).ToHaveTitleAsync(new Regex("CommitQuality"), new PageAssertionsToHaveTitleOptions { Timeout = 5000});
		//page.ScreenshotAsync(new PageScreenshotOptions
		//{
		//	Path = "screenshot.png",
		//	FullPage = true
		//});

		var first = Page.GetByTestId("name").First;

		//await Expect(first).Not.ToHaveTextAsync("Product 21");
	}

	[Test]
	public async Task Random_popup_Handler()
	{
		// If at any point a popup appears, close it
		await Page.AddLocatorHandlerAsync(Page.GetByText("Random Popup"), async () =>
		{
			await Page.GetByText("Close").ClickAsync();
		});

		await Page.GotoAsync("https://commitquality.com/practice-random-popup");
		await Task.Delay(6000); // wait until popup appears
		await Page.GetByTestId("accordion-1").ClickAsync(new() {Timeout = 2000 });
	}

	[Test]
	public async Task FileUpload()
	{
		var text = Environment.GetEnvironmentVariable("MYDATA");
		var text2 = TestContext.Parameters["GGEZ"];
		Assert.Warn($"111111111111111111-----| {text} {text2} |-----1111111111111");

		await Page.GotoAsync("https://commitquality.com/practice-file-upload");
		Page.Locator("input[type='file']").SetInputFilesAsync("C:\\Users\\Michal\\source\\repos\\LocatorsForWebElements\\PlaywrightTests\\chromeSettings.runsettings");

		// accept popup
		Page.Dialog += async (_, dialog) =>
		{
			//await Page.PauseAsync();
			await dialog.AcceptAsync();
		};
		//await Page.PauseAsync();

		await Page.Locator("button[type='submit']").ClickAsync();
	}

	[Test]
	public async Task Download_File()
	{
		await Page.GotoAsync("https://commitquality.com/practice-file-download");

		var waitForDownlaodTask = Page.WaitForDownloadAsync();

		await Page.GetByText("Download File").ClickAsync();
		var downlaod = await waitForDownlaodTask;

		await downlaod.SaveAsAsync("./../../../" + downlaod.SuggestedFilename);

	}

	[Test]
	public async Task Reuse_state()
	{
		await Page.GotoAsync("https://commitquality.com");
		var first = await Page.GetByTestId("navbar-logout").IsVisibleAsync();

		Assert.That(first, Is.True);
	}

	[TearDown]
	public async Task TearDown()
	{
		await context.CloseAsync();
		await browser.CloseAsync();
	}
}
