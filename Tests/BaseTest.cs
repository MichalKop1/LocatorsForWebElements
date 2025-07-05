using Business.Models;
using Core.Common;
using Core.Core;
using log4net;
using log4net.Config;
using RestSharp;

namespace Tests;

public abstract class BaseTest
{
	protected UserClient userClient;
	protected IRestClient client;

	protected ILog Log
	{
		get { return LogManager.GetLogger(this.GetType()); }
	}

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		log4net.Util.LogLog.InternalDebugging = true;
		XmlConfigurator.Configure(new FileInfo("Log.config"));

		client = new RestBuilder()
			.WithJsonSerializer()
			.WithRequest("/users")
			.Create(Constants.BaseUrl);

		userClient = new UserClient(client);
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		client.Dispose();
		userClient.Dispose();
		userClient = null!;
	}
}
