using Business.Models;
using System.Net;

namespace Core.Core;

public interface IUserClient
{
	Task<(List<User>, HttpStatusCode statusCode)> GetUsersAsync();
}
