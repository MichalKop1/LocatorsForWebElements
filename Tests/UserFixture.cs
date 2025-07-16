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
	[TestCase("users")]
	[Category("API")]
	public async Task VerifyThatUsersCanBeRetrieved(string endpoint)
	{
		var request = new RequestBuilder(endpoint)
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

	[TestCase("users", "Content-Type=application/json; charset=utf-8")]
	[Category("API")]
	public async Task VerifyResponseHeadersOfUsers(string endpoint, string expected)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		response.ContentHeaders.First().ToString().Should().Be(expected);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
		errMessagesExist.Should().BeFalse();

	}

	[TestCase("users",10)]
	[Category("API")]
	public async Task VerifyThatExpectedNumberOfUsersAreReturned(string endpoint, int expected)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var users = response.Data;
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		users.Count.Should().Be(expected);

		using (new AssertionScope())
		{
			users.Select(u => u.Id).Distinct().Count().Should().Be(users.Count);
			users.All(u => !string.IsNullOrEmpty(u.Name)).Should().BeTrue();
			users.All(u => !string.IsNullOrEmpty(u.Username)).Should().BeTrue();
		}

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		errMessagesExist.Should().BeFalse();
	}

	[TestCase("users","testName", "testUsername")]
	[Category("API")]
	public async Task VerifyThatUserCanBeCreated(string endpoint, string name, string username)
	{
		var user = new User()
		{
			Name = name,
			Username = username,
		};

		var request = new RequestBuilder(endpoint)
			.WithJsonBody(user)
			.WithMethod(Method.Post)
			.Build();

		var response = await userClient.PostUserAsync(user, request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		response.Should().NotBeNull();
		response.Data.Id.Should().BePositive();
		response.StatusCode.Should().Be(HttpStatusCode.Created);
		errMessagesExist.Should().BeFalse();
	}

	[TestCase("/invalidendpoint")]
	[Category("API")]
	public async Task VerifyNotFoundReturnedForInvalidEndpoint(string endpoint)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		response.Data.Should().BeNull();
		errMessagesExist.Should().BeTrue();
	}
}
