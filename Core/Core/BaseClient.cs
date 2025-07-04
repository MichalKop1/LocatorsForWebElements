using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Business.Models;

namespace Core.Core;

public class BaseClient
{
	private readonly IRestClient _client;

	public BaseClient(string endpoint)
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

	public async Task<List<User>> GetUsersAsync()
	{
		var request = new RestRequest("/users", Method.Get);
		var response = await _client.GetAsync<List<User>>(request);

		return response ?? new();
	}
}
