using Business.Models;
using log4net;
using RestSharp;
using System.Net;

namespace Core.Core;

/// <summary>
/// Provides functionality for interacting with a REST API to manage user-related operations.
/// </summary>
/// <remarks>The <see cref="UserClient"/> class is designed to facilitate communication with a REST API for user
/// management. It supports operations such as retrieving a list of users and creating new users. This class implements
/// <see cref="IDisposable"/> to ensure proper cleanup of resources.</remarks>
public class UserClient : IUserClient, IDisposable
{
	private readonly IRestClient _client;
	private bool _disposed;

	protected ILog Log => LogManager.GetLogger(this.GetType());

	public UserClient(IRestClient client)
	{
		Log.Info("Creating UserClient with provided RestClient.");
		_client = client;
	}

	/// <summary>
	/// Retrieves a list of users from the API.
	/// </summary>
	/// <param name="request">The REST request to execute.</param>
	/// <returns>A RestResponse containing the list of users.</returns>
	public async Task<RestResponse<List<User>>> GetUsersAsync(RestRequest request)
	{
		Log.Info("Retrieving users from the API.");
		var response = await _client.ExecuteAsync<List<User>>(request);

		if (response.StatusCode != HttpStatusCode.OK)
		{
			var errMessage = $"Failed to retrieve users. Status code: {response.StatusCode}, Error: {response.ErrorMessage}";
			Log.Error(errMessage);
		}
		else
		{
			int numberofUsers = response.Data != null ? response.Data.Count : 0;
			var message = $"Successfully retrieved {numberofUsers} users.";
			Log.Info(message);
		}

		return response;
	}

	/// <summary>
	/// Sends a POST request to create a new user and returns the server's response.
	/// </summary>
	/// <remarks>The method logs the operation's progress and outcome. If the user creation fails, the response will
	/// contain the error details.</remarks>
	/// <param name="user">The <see cref="User"/> object containing the details of the user to be created. Cannot be null.</param>
	/// <param name="request">The <see cref="RestRequest"/> object configured for the POST operation. Must include the target endpoint.</param>
	/// <returns>A <see cref="RestResponse"/> containing the server's response, including the created <see cref="User"/> object
	/// if successful.</returns>
	public async Task<RestResponse<User>> PostUserAsync(User user, RestRequest request)
	{
		var message = $"Posting user {user.Username}";
		Log.Info(message);
		request.AddJsonBody(user);

		var response = await _client.ExecuteAsync<User>(request);

		if (response.StatusCode != HttpStatusCode.Created)
		{
			var errMessage = $"Failed to create user. Status code: {response.StatusCode}, Error: {response.ErrorMessage}";
			Log.Error(errMessage);
		}
		else
		{
			message = $"User created successfully with ID: {response.Data!.Id}";
			Log.Info(message);
		}

		return response;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				Log.Info("Disposing UserClient.");
				_client?.Dispose();
			}

			_disposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}