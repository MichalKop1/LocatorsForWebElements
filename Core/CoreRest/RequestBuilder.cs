using RestSharp;

namespace Core.Core;

/// <summary>
/// A class for building REST requests with various configurations. 
/// Uses <see cref="Method.Get"/> by default.
/// </summary>
public class RequestBuilder
{
	private readonly RestRequest _request;

	public RequestBuilder(string endpoint)
	{
		_request = new RestRequest(endpoint, Method.Get);
	}

	/// <summary>
	/// Return an instance of <see cref="RestRequest"/> with the configured settings.
	/// </summary>
	/// <returns></returns>
	public RestRequest Build()
	{
		return _request;
	}

	public RequestBuilder WithHeader(string name, string value)
	{
		_request.AddHeader(name, value);
		return this;
	}

	public RequestBuilder WithParameter(string name, string value)
	{
		_request.AddParameter(name, value);
		return this;
	}

	public RequestBuilder WithQueryParameter(string name, string value)
	{
		_request.AddQueryParameter(name, value);
		return this;
	}

	public RequestBuilder WithUrlSegment(string name, string value)
	{
		_request.AddUrlSegment(name, value);
		return this;
	}

	public RequestBuilder WithJsonBody(object body)
	{
		_request.AddJsonBody(body);
		return this;
	}

	/// <summary>
	/// Allows setting the HTTP method for the request.
	/// </summary>
	/// <returns></returns>
	public RequestBuilder WithMethod(Method method)
	{
		_request.Method = method;
		return this;
	}
}
