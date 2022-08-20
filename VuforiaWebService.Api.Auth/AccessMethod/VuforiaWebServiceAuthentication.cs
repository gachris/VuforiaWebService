using System.Net.Http;
using System.Net.Http.Headers;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// OAuth 2.0 helper for accessing protected resources using the Bearer token as specified in
/// http://tools.ietf.org/html/rfc6750.
/// </summary>
public class VuforiaWebServiceAuthentication
{
    /// <summary>
    /// Thread-safe OAuth 2.0 method for accessing protected resources using the Authorization header as specified
    /// in http://tools.ietf.org/html/rfc6750#section-2.1.
    /// </summary>
    public class AuthorizationHeaderAccessMethod : IAccessMethod
    {
        private const string Schema = "VWS";

        public void Intercept(HttpRequestMessage request, string accessToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("VWS", accessToken);
        }

        public string GetAccessToken(HttpRequestMessage request)
        {
            return request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "VWS"
                ? request.Headers.Authorization.Parameter
                : null;
        }
    }
}
