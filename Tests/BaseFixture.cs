using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public abstract class BaseFixture
{
	protected UserClient userClient;

	[OneTimeSetUp]
	public void Setup()
	{
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Build();

		userClient = new UserClient(client);
	}


	[OneTimeTearDown]
	public void TearDown()
	{
		if (userClient != null)
		{
			userClient.Dispose();
			userClient = null;
		}
	}
}
