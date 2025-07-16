using Business.Models;
using RestSharp;
using System.Net;

namespace Core.Core;

public interface IUserClient
{
	Task<RestResponse<List<User>>> GetUsersAsync(RestRequest request);
	Task<RestResponse<User>> PostUserAsync(User user, RestRequest request);
}
