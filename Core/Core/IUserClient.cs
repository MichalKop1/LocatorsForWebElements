using Business.Models;
using RestSharp;
using System.Net;

namespace Core.Core;

public interface IUserClient
{
	Task<RestResponse<List<User>>> GetUsersAsync();
	Task<RestResponse<User>> PostUserAsync(User user);
}
