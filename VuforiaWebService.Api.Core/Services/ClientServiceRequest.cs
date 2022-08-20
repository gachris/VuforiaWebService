using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Core.Services;

/// <summary>
/// Represents an abstract, strongly typed request base class to make requests to a service.
/// Supports a strongly typed response.
/// </summary>
/// <typeparam name="TResponse">The type of the response object</typeparam>
public abstract class ClientServiceRequest<TResponse> : IClientServiceRequest<TResponse>, IClientServiceRequest
{
    /// <summary>The class logger.</summary>
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ClientServiceRequest<TResponse>>();

    /// <summary>The service on which this request will be executed.</summary>
    private readonly IClientService _service;

    /// <summary>The Database Access Keys.</summary>
    private readonly DatabaseAccessKeys _keys;

    public abstract string MethodName { get; }

    public abstract string RestPath { get; }

    public abstract string HttpMethod { get; }

    public IDictionary<string, IParameter> RequestParameters { get; private set; }

    public IClientService Service { get { return _service; } }

    public DatabaseAccessKeys Keys { get { return _keys; } }

    /// <summary>Creates a new service request.</summary>
    protected ClientServiceRequest(IClientService service, DatabaseAccessKeys keys)
    {
        _service = service;
        _keys = keys;
    }

    /// <summary>
    /// Initializes request's parameters. Inherited classes MUST override this method to add parameters to the
    /// <see cref="P:VuforiaPortal.Apis.Requests.ClientServiceRequest`1.RequestParameters" /> dictionary.
    /// </summary>
    protected virtual void InitParameters()
    {
        RequestParameters = new Dictionary<string, IParameter>();
    }

    public TResponse Execute()
    {
        try
        {
            using HttpResponseMessage result = ExecuteUnparsedAsync(CancellationToken.None).Result;
            return ParseResponse(result).Result;
        }
        catch (AggregateException ex)
        {
            throw ex.InnerException;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Stream ExecuteAsStream()
    {
        try
        {
            return ExecuteUnparsedAsync(CancellationToken.None).Result.Content.ReadAsStreamAsync().Result;
        }
        catch (AggregateException ex)
        {
            throw ex.InnerException;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<TResponse> ExecuteAsync()
    {
        return await ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
    {
        TResponse response1;
        using (HttpResponseMessage response2 = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            response1 = await ParseResponse(response2).ConfigureAwait(false);
        }
        return response1;
    }

    public async Task<Stream> ExecuteAsStreamAsync()
    {
        return await ExecuteAsStreamAsync(CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponseMessage = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
        return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    /// <summary>Sync executes the request without parsing the result. </summary>
    private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = CreateRequest();
        return await _service.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Parses the response and deserialize the content into the requested response object. </summary>
    protected virtual async Task<TResponse> ParseResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return await _service.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
        VuforiaErrorResponse requestError = await _service.DeserializeError(response).ConfigureAwait(false);
        throw new VuforiaPortalApiException(_service.Name, requestError.ToString())
        {
            Error = requestError,
            HttpStatusCode = response.StatusCode
        };
    }

    public HttpRequestMessage CreateRequest(bool? overrideGZipEnabled = null)
    {
        HttpRequestMessage request = CreateBuilder().CreateRequest();
        object body = GetBody();
        _service.HttpClientInitializer.GenerateAccessToken(_service, Keys, HttpMethod, body, request.RequestUri.AbsolutePath);
        request.SetRequestSerailizedContent(_service, body);
        return request;
    }

    /// <summary>
    /// Creates the <see cref="T:VuforiaPortal.Apis.Requests.RequestBuilder" /> which is used to generate a request.
    /// </summary>
    /// <returns>
    /// A new builder instance which contains the HTTP method and the right Uri with its path and query parameters.
    /// </returns>
    private RequestBuilder CreateBuilder()
    {
        RequestBuilder requestBuilder = new RequestBuilder()
        {
            BaseUri = new Uri(Service.BaseUri),
            Path = RestPath,
            Method = HttpMethod
        };
        IDictionary<string, object> parameterDictionary = ParameterUtils.CreateParameterDictionary(this);
        AddParameters(requestBuilder, ParameterCollection.FromDictionary(parameterDictionary));
        return requestBuilder;
    }

    /// <summary>Generates the right URL for this request.</summary>
    protected string GenerateRequestUri()
    {
        return CreateBuilder().BuildUri().ToString();
    }

    protected virtual object GetBody()
    {
        return null;
    }

    /// <summary>Adds path and query parameters to the given <c>requestBuilder</c>.</summary>
    private void AddParameters(RequestBuilder requestBuilder, ParameterCollection inputParameters)
    {
        foreach (KeyValuePair<string, string> inputParameter in inputParameters)
        {
            if (!RequestParameters.TryGetValue(inputParameter.Key, out IParameter parameter))
                throw new VuforiaPortalApiException(Service.Name, string.Format("Invalid parameter \"{0}\" was specified", inputParameter.Key));
            string defaultValue = inputParameter.Value;
            if (!ParameterValidator.ValidateParameter(parameter, defaultValue))
                throw new VuforiaPortalApiException(Service.Name, string.Format("Parameter validation failed for \"{0}\"", parameter.Name));
            if (defaultValue == null)
                defaultValue = parameter.DefaultValue;
            string parameterType = parameter.ParameterType;
            if (!(parameterType == "path"))
            {
                if (parameterType == "query")
                {
                    if (!object.Equals(defaultValue, parameter.DefaultValue) || parameter.IsRequired)
                        requestBuilder.AddParameter(RequestParameterType.Query, inputParameter.Key, defaultValue);
                }
                else
                    throw new VuforiaPortalApiException(_service.Name, string.Format("Unsupported parameter type \"{0}\" for \"{1}\"", parameter.ParameterType, parameter.Name));
            }
            else
                requestBuilder.AddParameter(RequestParameterType.Path, inputParameter.Key, defaultValue);
        }
        foreach (IParameter parameter in RequestParameters.Values)
        {
            if (parameter.IsRequired && !inputParameters.ContainsKey(parameter.Name))
                throw new VuforiaPortalApiException(_service.Name, string.Format("Parameter \"{0}\" is missing", parameter.Name));
        }
    }
}
