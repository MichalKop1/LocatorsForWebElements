using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core;

public class RequestBuilder
{
	private RestRequest _request;

	public RequestBuilder(string endpoint)
	{
		_request = new RestRequest(endpoint, Method.Get);
	}

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

	public RequestBuilder WithMethod(Method method)
	{
		_request.Method = method;
		return this;
	}
}
