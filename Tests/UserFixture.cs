using Business.Models;
using Core.Core;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System.Net;


namespace Tests;

[Parallelizable(ParallelScope.Children)]
public class UserFixture : BaseFixture
{
	[TestCase()]
	[Category("API")]
	public async Task VerifyThatUsersCanBeRetrieved()
	{
		var request = new RequestBuilder("users")
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var users = response.Data;
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		using (new AssertionScope())
		{
			users.Should().OnlyContain(u => u.Id > 0, "all user IDs should be greater than 0");
			users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Name), "all users should have a Name");
			users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Username), "all users should have a Username");
			users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Email), "all users should have an Email");
			users.Should().OnlyContain(u => u.Address != null, "all users should have an Address");
			users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Phone), "all users should have a Phone");
			users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Website), "all users should have a Website");
			users.Should().OnlyContain(u => u.Company != null, "all users should have a Company");
		}

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		errMessagesExist.Should().BeFalse();
	}

	[TestCase("Content-Type=application/json; charset=utf-8")]
	[Category("API")]
	public async Task VerifyResponseHeadersOfUsers(string expected)
	{
		var request = new RequestBuilder("users")
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response.ContentHeaders.First().ToString(), Is.EqualTo(expected));
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase(10)]
	[Category("API")]
	public async Task VerifyThatExpectedNumberOfUsersAreReturned(int expected)
	{
		var request = new RequestBuilder("users")
			.Build();

		var response = await userClient.GetUsersAsync(request);
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
		var user = new User()
		{
			Name = name,
			Username = username,
		};

		var request = new RequestBuilder("users")
			.WithJsonBody(user)
			.WithMethod(Method.Post)
			.Build();

		var response = await userClient.PostUserAsync(user, request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response, Is.Not.Null);
		Assert.That(response.Data.Id, Is.Positive);
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		Assert.That(errMessagesExist, Is.False);
	}

	[TestCase("/invalidendpoint")]
	[Category("API")]
	public async Task VerifyNotFoundReturnedForInvalidEndpoint(string endpoint)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		Assert.That(response.Data, Is.Null);
		Assert.That(errMessagesExist, Is.True);
	}
}
