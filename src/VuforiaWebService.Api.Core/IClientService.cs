using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VuforiaWebService.Api.Core.Serialization;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Core;

public interface IClientService : IDisposable
{
    /// <summary>Gets the HTTP client which is used to create requests.</summary>
    ConfigurableHttpClient HttpClient { get; }

    /// <summary>
    /// Gets a HTTP client initializer which is able to custom properties on
    /// <see cref="T:VuforiaPortal.Apis.Http.ConfigurableHttpClient" /> and
    /// <see cref="T:VuforiaPortal.Apis.Http.ConfigurableMessageHandler" />.
    /// </summary>
    IConfigurableHttpClientInitializer HttpClientInitializer { get; }

    /// <summary>Gets the service name.</summary>
    string Name { get; }

    /// <summary>Gets the BaseUri of the service. All request paths should be relative to this URI.</summary>
    string BaseUri { get; }

    /// <summary>Gets the BasePath of the service.</summary>
    string BasePath { get; }

    /// <summary>Gets the supported features by this service.</summary>
    IList<string> Features { get; }

    /// <summary>Gets the application name to be used in the User-Agent header.</summary>
    string ApplicationName { get; }

    /// <summary>
    /// Sets the content of the request by the given body and the this service's configuration.
    /// First the body object is serialized by the Serializer and then, if GZip is enabled, the content will be
    /// wrapped in a GZip stream, otherwise a regular string stream will be used.
    /// </summary>
    void SetRequestSerailizedContent(HttpRequestMessage request, object body);

    /// <summary>Gets the Serializer used by this service.</summary>
    ISerializer Serializer { get; }

    /// <summary>Serializes an object into a string representation.</summary>
    string SerializeObject(object data);

    /// <summary>Deserializes a response into string.</summary>
    Task<string> DeserializeResponse(HttpResponseMessage response);

    /// <summary>Deserializes a response into the specified object.</summary>
    Task<T> DeserializeResponse<T>(HttpResponseMessage response);

    /// <summary>Deserializes an error response into a <see cref="T:VuforiaPortal.Apis.Requests.RequestError" /> object.</summary>
    /// <exception cref="T:VuforiaPortal.VuforiaPortalApiException">If no error is found in the response.</exception>
    Task<VuforiaErrorResponse> DeserializeError(HttpResponseMessage response);
}
