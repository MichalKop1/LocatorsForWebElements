using log4net;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Core;

public class RestBuilder
{
	private IRestClient _client;
	private JsonSerializerOptions _serializerOptions;

	protected ILog Log => LogManager.GetLogger(this.GetType());

	static RestBuilder()
	{
		log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.config"));
	}

	public RestBuilder WithJsonSerializer()
	{
		AddJsonSerializer();

		var client = new RestClient(
			baseUrl: Constants.Constants.BaseUrl,
			configureSerialization: s => s.UseSystemTextJson(_serializerOptions)
			);

		_client = client;
		return this;
	}

	public RestBuilder WithDefaultHeader(string name, string value)
	{
		if (_client == null)
		{
			var errMessage = "Client must be created before adding default headers.";
			Log.Error(errMessage);
			throw new InvalidOperationException(errMessage);
		}

		_client.AddDefaultHeader(name, value);

		return this;
	}

	public RestBuilder WithDefaultParameter(string name, string value)
	{
		if (_client == null)
		{
			var errMessage = "Client must be created before adding default headers.";
			Log.Error(errMessage);
			throw new InvalidOperationException(errMessage);
		}

		_client.AddDefaultParameter(name, value);
		return this;
	}

	public IRestClient Build()
	{
		if (_client == null)
		{
			var errMessage = "Client must be created before building.";
			Log.Error(errMessage);
			throw new InvalidOperationException(errMessage);
		}

		return _client;
	}

	private void AddJsonSerializer(JsonSerializerOptions? options = null)
	{
		var message = $"Setting up Json Serializer options: Json Ignore when writing null and property name case insensitive";
		Log.Info(message);

		_serializerOptions = options ?? new JsonSerializerOptions()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNameCaseInsensitive = true,
		};
	}
}
