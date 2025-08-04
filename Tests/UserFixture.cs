using Business.Models;
using Core.Core;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System.Net;

namespace RestTests;

[Parallelizable(ParallelScope.Children)]
public class UserFixture : BaseFixture
{
	[TestCase("users")]
	[Category("API")]
	[Category("Regression")]
	public async Task VerifyThatUsersCanBeRetrieved(string endpoint)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var users = response.Data;
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		using (new AssertionScope())
		{
			users.Should().AllSatisfy(u =>
			{
				u.Id.Should().BeGreaterThan(0, "all user IDs should be greater than 0");
				u.Name.Should().NotBeNullOrEmpty("all users should have a Name");
				u.Username.Should().NotBeNullOrEmpty("all users should have a Username");
				u.Email.Should().NotBeNullOrEmpty("all users should have an Email");
				u.Address.Should().NotBeNull("all users should have an Address");
				u.Phone.Should().NotBeNullOrEmpty("all users should have a Phone");
				u.Website.Should().NotBeNullOrEmpty("all users should have a Website");
				u.Company.Should().NotBeNull("all users should have a Company");
			});

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			errMessagesExist.Should().BeFalse();
		}
	}

	[TestCase("users", "Content-Type=application/json; charset=utf-8")]
	[Category("API")]
	public async Task VerifyResponseHeadersOfUsers(string endpoint, string expected)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		using (new AssertionScope())
		{
			response.ContentHeaders?.First().ToString().Should().Be(expected);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			errMessagesExist.Should().BeFalse();
		}
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

		using (new AssertionScope())
		{
			users?.Count.Should().Be(expected);

			users.Should().AllSatisfy(u =>
			{
				u.Name.Should().NotBeNullOrEmpty();
				u.Username.Should().NotBeNullOrEmpty();
			});

			users?.Select(u => u.Id).Should().OnlyHaveUniqueItems();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			errMessagesExist.Should().BeFalse();
		}
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

		using (new AssertionScope())
		{
			response.Should().NotBeNull();
			response.Data?.Id.Should().BePositive();
			response.StatusCode.Should().Be(HttpStatusCode.Created);
			errMessagesExist.Should().BeFalse();
		}
	}

	[TestCase("/invalidendpoint")]
	[Category("API")]
	public async Task VerifyNotFoundReturnedForInvalidEndpoint(string endpoint)
	{
		var request = new RequestBuilder(endpoint)
			.Build();

		var response = await userClient.GetUsersAsync(request);
		var errMessagesExist = response.ErrorMessage?.Length > 0;

		using (new AssertionScope())
		{
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
			response.Data.Should().BeNull();
			errMessagesExist.Should().BeTrue();
		}		
	}
}
