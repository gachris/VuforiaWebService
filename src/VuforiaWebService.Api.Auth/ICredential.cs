using VuforiaWebService.Api.Core;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// The main interface to represent credential in the client library.
/// Service account, User account and Compute credential inherit from this interface
/// to provide access token functionality. In addition this interface inherits from
/// <see cref="T:VuforiaPortal.Apis.Http.IConfigurableHttpClientInitializer" /> to be able to hook to http requests.
/// More details are available in the specific implementations.
/// </summary>
public interface ICredential : IConfigurableHttpClientInitializer
{
}
