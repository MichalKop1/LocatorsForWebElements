using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace Core.Core;

public class LoggingWebDriver : IWebDriver, IActionExecutor
{
	private readonly IWebDriver driver;
	private readonly IActionExecutor actionExecutor;

	protected ILog Log
	{
		get { return LogManager.GetLogger(GetType()); }
	}

	public LoggingWebDriver(IWebDriver driver)
	{
		this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
		actionExecutor = driver as IActionExecutor
			?? throw new ArgumentException("The IWebDriver object must implement IActionExecutor.", nameof(driver));
	}

	public string Url
	{
		get => driver.Url;
		set
		{
			var message = $"Navigating to URL: {value}";
			Log.Info(message);
			driver.Url = value;
		}
	}

	public string Title
	{
		get
		{
			string title = driver.Title;
			var message = $"Page title: {title}";
			Log.Info(message);
			return title;
		}
	}

	public string PageSource
	{
		get
		{
			Log.Info("Fetching PageSource...");
			return driver.PageSource;
		}
	}

	public string CurrentWindowHandle
	{
		get
		{
			string handle = driver.CurrentWindowHandle;
			var message = $"Current window handle: {handle}";
			Log.Info(message);
			return handle;
		}
	}

	public ReadOnlyCollection<string> WindowHandles
	{
		get
		{
			var handles = driver.WindowHandles;
			var message = $"Window handles count: {handles.Count}";
			Log.Info(message);
			return handles;
		}
	}

	public void Close()
	{
		Log.Info("Closing browser window...");
		driver.Close();
	}

	public void Quit()
	{
		Log.Info("Quitting WebDriver...");
		driver.Quit();
	}

	public IOptions Manage()
	{
		Log.Info("Accessing browser options...");
		return driver.Manage();
	}

	public INavigation Navigate()
	{
		Log.Info("Accessing navigation commands...");
		return driver.Navigate();
	}

	public ITargetLocator SwitchTo()
	{
		Log.Info("Switching context...");
		return driver.SwitchTo();
	}

	public IWebElement FindElement(By by)
	{
		var message = $"Finding element by: {by}";
		Log.Info(message);
		return driver.FindElement(by);
	}

	public ReadOnlyCollection<IWebElement> FindElements(By by)
	{
		var message = $"Finding elements by: {by}";
		Log.Info(message);
		return driver.FindElements(by);
	}

	public void Dispose()
	{
		Log.Info("Disposing WebDriver...");
		driver.Dispose();
	}

	public void PerformActions(IList<ActionSequence> actionSequenceList)
	{
		var actionsString = string.Join(", ", actionSequenceList);
		var message = $"Performing {actionsString}";
		Log.Info(message);
		actionExecutor.PerformActions(actionSequenceList);
	}

	public void ResetInputState()
	{
		Log.Info("Resetting input state...");
		actionExecutor.ResetInputState();
	}

	public bool IsActionExecutor => actionExecutor != null;
}