using Business.Models;
using log4net;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Core;

public class UserClient : IUserClient, IDisposable
{
	private readonly IRestClient _client;
	

	protected ILog Log => LogManager.GetLogger(this.GetType());

	public UserClient(IRestClient client)
	{
		Log.Info("Creating UserClient with provided RestClient.");
		_client = client;
	}

	public async Task<RestResponse<List<User>>> GetUsersAsync()
	{
		Log.Info("Retrieving users from the API.");
		var request = new RestRequest("/users", Method.Get);
		var response = await _client.ExecuteAsync<List<User>>(request);

		if (response.StatusCode != HttpStatusCode.OK)
		{
			var errMessage = $"Failed to retrieve users. Status code: {response.StatusCode}, Error: {response.ErrorMessage}";
			Log.Error(errMessage);
		}
		else
		{
			var message = $"Successfully retrieved {response.Data.Count} users.";
			Log.Info($"Successfully retrieved {response.Data.Count} users.");
		}

		return response;
	}

	public async Task<RestResponse<User>> PostUserAsync(User user)
	{
		var message = $"Posting user {user.Id}.{user.Username}";
		Log.Info(message);
		var request = new RestRequest("/users", Method.Post);
		request.AddJsonBody(user);

		var response = await _client.ExecuteAsync<User>(request);

		if (response.StatusCode != HttpStatusCode.Created)
		{
			var errMessage = $"Failed to create user. Status code: {response.StatusCode}, Error: {response.ErrorMessage}";
			Log.Error(errMessage);
		}
		else
		{
			message = $"User created successfully with ID: {response.Data.Id}";
			Log.Info(message);
		}

		return response;
	}

	

	public void Dispose()
	{
		Log.Info("Disposing UserClient.");
		_client?.Dispose();
	}
}