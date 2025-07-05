using log4net;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Core;

public class RestFactory
{
	private IRestClient _client;
	private RestRequest _request;
	private JsonSerializerOptions _serializerOptions;
	protected ILog Log => LogManager.GetLogger(this.GetType());

	static RestFactory()
	{
		log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("Log.config"));
	}

	public IRestClient Create()
	{
		Log.Info("Creating RestClient without base URL.");
		_client = new RestClient();
		return _client;
	}

	public IRestClient Create(string baseUrl)
	{
		if (_serializerOptions == null)
		{
			var errMessage = "Serializer options must be set before creating the RestClient.";
			Log.Error(errMessage);
			throw new InvalidOperationException(errMessage);
		}

		var message = $"Creating RestClient with System.Text.Json and base URL: {baseUrl}";
		Log.Info(message);

		_client = new RestClient(
			options: new() {BaseUrl = new(baseUrl) },
			configureSerialization: s => s.UseSystemTextJson(_serializerOptions)
			);
		return _client;
	}

	public RestFactory WithRequest(string resource)
	{
		if (string.IsNullOrWhiteSpace(resource))
		{
			var errorMessage = "Resource cannot be null or empty.";
			Log.Error(errorMessage);
			throw new ArgumentException(errorMessage, nameof(resource));
		}
		else if (!resource.StartsWith('/'))
		{
			var errorMessage = $"Resource must start with a forward slash ('/'). Provided resource: {resource}";
			Log.Error(errorMessage);
			throw new ArgumentException(errorMessage, nameof(resource));
		}

		var message = $"Setting up the request with '{resource}' resource";
		Log.Info(message);
		_request = new RestRequest(resource);
		return this;
	}

	public RestFactory WithJsonSerializer()
	{
		var message = $"Setting up Json Serializer options: Json Ignore when writing null and property name case insensitive";
		Log.Info(message);

		_serializerOptions = new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNameCaseInsensitive = true,
		};
		return this;
	}

	public RestFactory WithHeader(string name, string value)
	{
		var message = $"Adding header. Name: {name} | Value: {value}";
		_request.AddHeader(name, value);
		return this;
	}
}
