using log4net;
using NUnit.Framework.Interfaces;

namespace WebDriverTests.Common;

/// <summary>
/// An attribute that logs time before and after the execution of a test method.
/// </summary>
/// <remarks>This attribute is used to log messages indicating the start and completion of a test method.  It
/// implements the <see cref="ITestAction"/> interface, allowing it to hook into the test execution lifecycle.
/// The attribute can be used only once per method.</remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LogAttribute : NUnitAttribute, ITestAction
{
	protected ILog Log => LogManager.GetLogger(this.GetType());
	public void AfterTest(ITest test)
	{
		Log.Info($"Test \"{test.Name}\" completed.");
	}

	public void BeforeTest(ITest test)
	{
		Log.Info($"Starting test: {test.Name}");
	}

	public ActionTargets Targets => ActionTargets.Test;
}
