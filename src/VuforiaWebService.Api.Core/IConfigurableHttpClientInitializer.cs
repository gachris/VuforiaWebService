using System.Net;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Core;

public interface IConfigurableHttpClientInitializer
{
    NetworkCredential NetworkCredential { get; }

    /// <summary>Initializes a HTTP client after it was created.</summary>
    void Initialize(ConfigurableHttpClient httpClient);

    void GenerateAccessToken(IClientService clientService, DatabaseAccessKeys keys, string httpMethod, object body, string requestPath);
}
