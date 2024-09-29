using System.Net.Http.Headers;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// Helper for accessing protected resources using Authentication.
/// </summary>
public class VuforiaWebServiceAuthentication
{
    /// <summary>
    /// Method for accessing protected resources using the Authorization header.
    /// </summary>
    public class AuthorizationHeaderAccessMethod : IAccessMethod
    {
        private const string Schema = "VWS";

        /// <inheritdoc/>
        public void Intercept(HttpRequestMessage request, string accessToken) => request.Headers.Authorization = new AuthenticationHeaderValue(Schema, accessToken);

        /// <inheritdoc/>
        public string GetAccessToken(HttpRequestMessage request)
        {
            return request.Headers.Authorization != null && request.Headers.Authorization.Scheme == Schema
                ? request.Headers.Authorization.Parameter
                : null;
        }
    }
}
