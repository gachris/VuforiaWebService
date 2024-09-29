using System.Net;

namespace VuforiaWebService.Api.Core;

/// <summary>
/// An interface for initializing a <see cref="ConfigurableHttpClient"/> after it was created.
/// </summary>
public interface IConfigurableHttpClientInitializer
{
    /// <summary>
    /// Initializes a <see cref="ConfigurableHttpClient"/> by configuring its properties.
    /// </summary>
    /// <param name="httpClient">The <see cref="ConfigurableHttpClient"/> instance to be initialized.</param>
    void Initialize(ConfigurableHttpClient httpClient);

    /// <summary>
    /// Generates an access token based on the provided parameters using HMAC-SHA1 hashing.
    /// </summary>
    /// <param name="clientService">The client service used for serialization and making requests.</param>
    /// <param name="keys">The database access keys used for authentication.</param>
    /// <param name="httpMethod">The HTTP method of the request (e.g., GET, POST).</param>
    /// <param name="body">The body of the request to be serialized.</param>
    /// <param name="requestPath">The path of the request being made.</param>
    void GenerateAccessToken(IClientService clientService, DatabaseAccessKeys keys, string httpMethod, object body, string requestPath);
}
