using VuforiaWebService.Api.Core.Extensions;
using VuforiaWebService.Api.Core.Logger;
using VuforiaWebService.Api.Core.Response;
using VuforiaWebService.Api.Core.Utils;

namespace VuforiaWebService.Api.Core.Request;

/// <summary>
/// Represents an abstract, strongly typed request base class to make requests to a service.
/// Supports a strongly typed response.
/// </summary>
/// <typeparam name="TResponse">The type of the response object</typeparam>
public abstract class ClientServiceRequest<TResponse> : IClientServiceRequest<TResponse>, IClientServiceRequest
{
    private static readonly ILogger _logger = ApplicationContext.Logger.ForType<ClientServiceRequest<TResponse>>();

    private readonly Dictionary<string, IParameter> _requestParameters = [];

    /// <summary>
    /// The service on which this request will be executed.
    /// </summary>
    private readonly IClientService _service;

    /// <summary>
    /// The Database Access Keys.
    /// </summary>
    private readonly DatabaseAccessKeys _keys;

    /// <inheritdoc/>
    public abstract string MethodName { get; }

    /// <inheritdoc/>
    public abstract string RestPath { get; }

    /// <inheritdoc/>
    public abstract string HttpMethod { get; }

    /// <inheritdoc/>
    public IDictionary<string, IParameter> RequestParameters => _requestParameters;

    /// <inheritdoc/>
    public IClientService Service => _service;

    /// <summary>
    /// Gets the Database Access Keys.
    /// </summary>
    public DatabaseAccessKeys Keys => _keys;

    /// <summary>
    /// Creates a new service request.
    /// </summary>
    protected ClientServiceRequest(IClientService service, DatabaseAccessKeys keys)
    {
        _service = service;
        _keys = keys;

        InitParameters();
    }

    /// <summary>
    /// Initializes request's parameters. Inherited classes MUST override this method to add parameters to the
    /// <see cref="RequestParameters" /> dictionary.
    /// </summary>
    protected virtual void InitParameters()
    {
    }

    /// <inheritdoc/>
    public TResponse Execute()
    {
        try
        {
            using var result = ExecuteUnparsedAsync(CancellationToken.None).Result;
            return ParseResponse(result).Result;
        }
        catch (AggregateException ex)
        {
            throw ex.InnerException ?? ex;
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc/>
    public Stream ExecuteAsStream()
    {
        try
        {
            return ExecuteUnparsedAsync(CancellationToken.None).Result.Content.ReadAsStreamAsync().Result;
        }
        catch (AggregateException ex)
        {
            throw ex.InnerException ?? ex;
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<TResponse> ExecuteAsync() => await ExecuteAsync(CancellationToken.None).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
    {
        TResponse response;
        using (var httpResponseMessage = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            response = await ParseResponse(httpResponseMessage).ConfigureAwait(false);
        }
        return response;
    }

    /// <inheritdoc/>
    public async Task<Stream> ExecuteAsStreamAsync() => await ExecuteAsStreamAsync(CancellationToken.None).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
        return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    /// <summary>Sync executes the request without parsing the result. </summary>
    private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken cancellationToken)
    {
        using var request = CreateRequest();
        return await _service.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Parses the response and deserialize the content into the requested response object. </summary>
    private async Task<TResponse> ParseResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await _service.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
        }
        var requestError = await _service.DeserializeError(response).ConfigureAwait(false);
        throw new VuforiaPortalApiException(_service.Name, requestError.ToString())
        {
            Error = requestError,
            HttpStatusCode = response.StatusCode
        };
    }

    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest()
    {
        var request = CreateBuilder().CreateRequest();
        var body = GetBody();

        _service.HttpClientInitializer.GenerateAccessToken(_service, Keys, HttpMethod, body, request.RequestUri.AbsolutePath);

        request.SetRequestSerailizedContent(_service, body);

        return request;
    }

    /// <summary>
    /// Creates the <see cref="RequestBuilder" /> which is used to generate a request.
    /// </summary>
    /// <returns>
    /// A new builder instance which contains the HTTP method and the right Uri with its path and query parameters.
    /// </returns>
    private RequestBuilder CreateBuilder()
    {
        var requestBuilder = new RequestBuilder(new Uri(Service.BaseUri), RestPath, HttpMethod);
        var parameterDictionary = ParameterUtils.CreateParameterDictionary(this);
        AddParameters(requestBuilder, ParameterCollection.FromDictionary(parameterDictionary));
        return requestBuilder;
    }

    ///<summary>Returns the body of the request.</summary>
    protected virtual object GetBody() => default;

    /// <summary>Adds path and query parameters to the given <c>requestBuilder</c>.</summary>
    private void AddParameters(RequestBuilder requestBuilder, ParameterCollection inputParameters)
    {
        inputParameters.ForEach(inputParameter =>
        {
            if (!RequestParameters.TryGetValue(inputParameter.Key, out var parameter))
                throw new VuforiaPortalApiException(Service.Name, string.Format("Invalid parameter \"{0}\" was specified", inputParameter.Key));

            var defaultValue = inputParameter.Value;

            if (!ParameterValidator.ValidateParameter(parameter, defaultValue))
                throw new VuforiaPortalApiException(Service.Name, string.Format("Parameter validation failed for \"{0}\"", parameter.Name));

            if (defaultValue == null)
            {
                defaultValue = parameter.DefaultValue;
            }
            var parameterType = parameter.ParameterType;
            if (!(parameterType == "path"))
            {
                if (parameterType == "query")
                {
                    if (!Equals(defaultValue, parameter.DefaultValue) || parameter.IsRequired)
                        requestBuilder.AddParameter(RequestParameterType.Query, inputParameter.Key, defaultValue);
                }
                else throw new VuforiaPortalApiException(_service.Name,
                                                         string.Format("Unsupported parameter type \"{0}\" for \"{1}\"",
                                                         parameter.ParameterType,
                                                         parameter.Name));
            }
            else requestBuilder.AddParameter(RequestParameterType.Path, inputParameter.Key, defaultValue);
        });

        RequestParameters.Values.ForEach(parameter =>
        {
            if (parameter.IsRequired && !inputParameters.ContainsKey(parameter.Name))
                throw new VuforiaPortalApiException(_service.Name, string.Format("Parameter \"{0}\" is missing", parameter.Name));
        });
    }
}
