using log4net;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Core;

public class RestBuilder
{
	private IRestClient _client;
	private RestRequest _request;
	private JsonSerializerOptions _serializerOptions;

	protected ILog Log => LogManager.GetLogger(this.GetType());

	static RestBuilder()
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

	public RestBuilder WithRequest(string resource)
	{
		if (_client == null)
		{
			var errorMessage = "RestClient must be created before setting up a request.";
			Log.Error(errorMessage);
			throw new InvalidOperationException(errorMessage);
		}

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

	public RestBuilder WithParameter(string name, string value)
	{
		if (_request == null)
		{
			var errorMessage = "Request must be set up before adding parameters.";
			Log.Error(errorMessage);
			throw new InvalidOperationException(errorMessage);
		}
		else if (string.IsNullOrWhiteSpace(name))
		{
			var errorMessage = "Parameter name cannot be null or empty.";
			Log.Error(errorMessage);
			throw new ArgumentException(errorMessage, nameof(name));
		}
		else if (string.IsNullOrEmpty(name))
		{
			var errorMessage = "Parameter value cannot be null.";
			Log.Error(errorMessage);
			throw new ArgumentNullException(nameof(value), errorMessage);
		}

		_request.AddParameter(name, value);
		return this;
	}

	public RestBuilder WithJsonSerializer()
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

	public RestBuilder WithHeader(string name, string value)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			var errorMessage = "Header name cannot be null or empty.";
			Log.Error(errorMessage);
			throw new ArgumentException(errorMessage, nameof(name));
		}
		else if (string.IsNullOrEmpty(value))
		{
			var errorMessage = "Header value cannot be null.";
			Log.Error(errorMessage);
			throw new ArgumentNullException(nameof(value), errorMessage);
		}

		var message = $"Adding header. Name: {name} | Value: {value}";
		Log.Info(message);
		_request.AddHeader(name, value);
		return this;
	}
}
