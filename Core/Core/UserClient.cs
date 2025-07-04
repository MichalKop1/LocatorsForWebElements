using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Business.Models;
using System.Net;

namespace Core.Core;

public class UserClient : IUserClient
{
	private readonly IRestClient _client;

	public UserClient(string endpoint)
	{
		var serializerOptions = new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNameCaseInsensitive = true,
		};

		_client = new RestClient(
			options: new() { BaseUrl = new(endpoint) },
			configureSerialization: s => s.UseSystemTextJson(serializerOptions));
	}

	public async Task<(List<User>, HttpStatusCode statusCode)> GetUsersAsync()
	{
		var request = new RestRequest("/users", Method.Get);
		var response = await _client.ExecuteAsync<List<User>>(request);

		return (response.Data ?? new(), response.StatusCode);
	}
}