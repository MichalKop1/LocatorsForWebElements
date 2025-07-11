using log4net;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Core;

public class RestBuilder
{
	private const string DefaultHeader = "DefaultHeader";
	private const string DefaultParameter = "DefaultParameter";
	private const string DefaultQueryParameter = "DefaultQueryParameter";
	private const string DefaultUrlSegment = "DefaultUrlSegment";

	private IRestClient _client;
	private JsonSerializerOptions _serializerOptions;

	private Dictionary<string, List<(string, string)>> _defaultClientRequests = new();
	protected ILog Log => LogManager.GetLogger(this.GetType());

	public RestBuilder()
	{
		_defaultClientRequests.Add(DefaultHeader, new List<(string, string)>());
		_defaultClientRequests.Add(DefaultParameter, new List<(string, string)>());
		_defaultClientRequests.Add(DefaultQueryParameter, new List<(string, string)>());
		_defaultClientRequests.Add(DefaultUrlSegment, new List<(string, string)>());
	}

	static RestBuilder()
	{
		log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.config"));
	}

	public IRestClient Create()
	{
		Log.Info("Creating RestClient without base URL.");
		_client = new RestClient();

		return _client;
	}

	public IRestClient Create(string baseUrl, IRestSerializer? serializer = null)
	{
		if (_serializerOptions == null)
		{
			var errMessage = "Serializer options must be set before creating the RestClient.";
			Log.Error(errMessage);
			throw new InvalidOperationException(errMessage);
		}

		var message = $"Creating RestClient with System.Text.Json and base URL: {baseUrl}";
		Log.Info(message);

		var client = new RestClient(
			options: new() {BaseUrl = new(baseUrl) },
			configureSerialization: s => s.UseSystemTextJson(_serializerOptions)
			);

		foreach(var defaultRequest in _defaultClientRequests)
		{
			foreach (var (name, value) in defaultRequest.Value)
			{
				switch (defaultRequest.Key)
				{
					case DefaultHeader:
						_client.AddDefaultHeader(name, value);
						break;
					case DefaultParameter:
						_client.AddDefaultParameter(name, value);
						break;
					case DefaultQueryParameter:
						_client.AddDefaultQueryParameter(name, value);
						break;
					case DefaultUrlSegment:
						_client.AddDefaultUrlSegment(name, value);
						break;
				}
			}
		}

		return client;
	}

	public RestBuilder WithDefaultHeader(string name, string value)
	{
		_defaultClientRequests[DefaultHeader].Add((name, value));
		return this;
	}

	public RestBuilder WithDefaultParameter(string name, string value)
	{
		_defaultClientRequests[DefaultParameter].Add((name, value));
		return this;
	}

	public RestBuilder WithDefaultQueryParameter(string name, string value)
	{
		_defaultClientRequests[DefaultQueryParameter].Add((name, value));
		return this;
	}

	public RestBuilder WithDefaultUrlSegment(string name, string value)
	{
		_defaultClientRequests[DefaultUrlSegment].Add((name, value));
		return this;
	}

	public RestBuilder WithJsonSerializer(JsonSerializerOptions? options = null)
	{
		var message = $"Setting up Json Serializer options: Json Ignore when writing null and property name case insensitive";
		Log.Info(message);

		_serializerOptions = options ?? new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNameCaseInsensitive = true,
		};
		return this;
	}
}
