using NUnit.Framework;
using Core.Core;
using Core.Common;
using RestSharp;
using System.Net;
namespace Tests;

public class UserFixture
{
	private readonly UserClient userClient = new(Constants.BaseUrl);

	[Test]
	public async Task VerifyThatUsersCanBeRetrieved()
	{
		var (users, statusCode) = await userClient.GetUsersAsync();
		
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
		Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
	}
}
