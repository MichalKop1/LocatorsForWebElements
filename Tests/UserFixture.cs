using Business.Models;
using Core.Core;
using System.Net;
using Tests.TestData;
namespace Tests;

[Parallelizable(ParallelScope.Children)]
public class UserFixture
{
	[TestCase()]
	[Category("API")]
	public async Task VerifyThatUsersCanBeRetrieved()
	{
		// try DI container
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Create(Constants.BaseUrl);
		var userClient = new UserClient(client);

		var response = await userClient.GetUsersAsync();
		var users = response.Data;
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		// try smart assertions
		Assert.Multiple(() =>
		{
			Assert.That(users.All(u => u.Id > 0));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Name)));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Username)));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Email)));
			Assert.That(users.All(u => u.Address != null));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Phone)));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Website)));
			Assert.That(users.All(u => u.Company != null));
		});
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase("Content-Type=application/json; charset=utf-8")]
	[Category("API")]
	public async Task VerifyResponseHeadersOfUsers(string expected)
	{
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Create(Constants.BaseUrl);
		var userClient = new UserClient(client);

		var response = await userClient.GetUsersAsync();
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response.ContentHeaders.First().ToString(), Is.EqualTo(expected));
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase(10)]
	[Category("API")]
	public async Task VerifyThatExpectedNumberOfUsersAreReturned(int expected)
	{
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Create(Constants.BaseUrl);
		var userClient = new UserClient(client);

		var response = await userClient.GetUsersAsync();
		var users = response.Data;
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(users.Count, Is.EqualTo(expected));

		Assert.Multiple(() =>
		{
			Assert.That(users.Select(u => u.Id).Distinct().Count(), Is.EqualTo(users.Count));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Name)));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Username)));
		});

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase("testName", "testUsername")]
	[Category("API")]
	public async Task VerifyThatUserCanBeCreated(string name, string username)
	{
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Create(Constants.BaseUrl);
		var userClient = new UserClient(client);

		var user = new User()
		{
			Name = name,
			Username = username,
		};

		var response = await userClient.PostUserAsync(user);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response, Is.Not.Null);
		Assert.That(response.Data.Id, Is.Positive);
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase("https://jsonplaceholder.typicode.com/invalidendpoint")]
	[Category("API")]
	public async Task VerifyNotFoundReturnedForInvalidEndpoint(string url)
	{
		var client = new RestBuilder()
			.WithJsonSerializer()
			.Create(url);

		var userClient = new UserClient(client);
		var response = await userClient.GetUsersAsync();
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		Assert.That(response.Data, Is.Null);
		Assert.That(errMessagesExist, Is.True);
	}
}
