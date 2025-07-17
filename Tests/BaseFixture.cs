using Core.Core;

namespace RestTests;

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
