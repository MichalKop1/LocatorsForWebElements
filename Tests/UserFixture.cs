using NUnit.Framework;
using Core.Core;
using Core.Common;
using RestSharp;
using System.Net;
using Business.Models;
namespace Tests;

[Parallelizable(ParallelScope.Children)]
public class UserFixture
{
	private UserClient userClient;
	private IRestClient client;

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		client = new RestFactory()
			.WithJsonSerializer()
			.WithRequest("/users")
			.Create(Constants.BaseUrl);
			
		userClient = new UserClient(client);
	}

	[Test]
	public async Task VerifyThatUsersCanBeRetrieved()
	{
		var response = await userClient.GetUsersAsync();
		var users = response.Data;

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
	}

	[TestCase("Content-Type=application/json; charset=utf-8")]
	public async Task VerifyResponseHeadersOfUsers(string expected)
	{
		var response = await userClient.GetUsersAsync();

		Assert.That(response.ContentHeaders.First().ToString(), Is.EqualTo(expected));
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[TestCase(10)]
	public async Task VerifyThatExpectedNumberOfUsersAreReturned(int expected)
	{
		var response = await userClient.GetUsersAsync();
		var users = response.Data;

		Assert.That(users.Count, Is.EqualTo(expected));

		Assert.Multiple(() =>
		{
			Assert.That(users.Select(u => u.Id).Distinct().Count(), Is.EqualTo(users.Count));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Name)));
			Assert.That(users.All(u => !string.IsNullOrEmpty(u.Username)));
		});

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[TestCase("testName", "testUsername")]
	public async Task VerifyThatUserCanBeCreated(string name, string username)
	{
		var user = new User()
		{
			Name = name,
			Username = username,
		};

		var response = await userClient.PostUserAsync(user);

		Assert.That(response, Is.Not.Null);
		Assert.That(response.Data.Id, Is.Positive);
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
	}

	[TestCase("https://jsonplaceholder.typicode.com/invalidendpoint")]
	public async Task VerifyNotFoundReturnedForInvalidEndpoint(string url)
	{
		var client = new RestFactory()
			.WithJsonSerializer()
			.WithRequest("/users")
			.Create(url);

		var userClient = new UserClient(client);
		var response = await userClient.GetUsersAsync();

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		client.Dispose();
		userClient = null!;
	}
}
