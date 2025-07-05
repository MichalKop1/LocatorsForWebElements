using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Core;

public class RestFactory
{
	private IRestClient _client;
	private RestRequest _request;
	private JsonSerializerOptions _serializerOptions;

	public IRestClient Create()
	{
		_client = new RestClient();
		return _client;
	}

	public IRestClient Create(string baseUrl)
	{
		_client = new RestClient(
			options: new() {BaseUrl = new(baseUrl) },
			configureSerialization: s => s.UseSystemTextJson(_serializerOptions)
			);
		return _client;
	}

	public RestFactory WithRequest(string resource)
	{
		_request = new RestRequest(resource);
		return this;
	}

	public RestFactory WithJsonSerializer()
	{
		_serializerOptions = new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNameCaseInsensitive = true,
		};
		return this;
	}

	public RestFactory WithHeader(string name, string value)
	{
		_request.AddHeader(name, value);
		return this;
	}
}
