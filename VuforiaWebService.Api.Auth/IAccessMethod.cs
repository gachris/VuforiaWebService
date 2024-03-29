﻿using System.Net.Http;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// Method of presenting the access token to the resource server as specified in
/// http://tools.ietf.org/html/rfc6749#section-7
/// </summary>
public interface IAccessMethod
{
    /// <summary>
    /// Intercepts a HTTP request right before the HTTP request executes by providing the access token.
    /// </summary>
    void Intercept(HttpRequestMessage request, string accessToken);

    /// <summary>
    /// Retrieves the original access token in the HTTP request, as provided in the <see cref="M:VuforiaPortal.Apis.Auth.OAuth2.IAccessMethod.Intercept(System.Net.Http.HttpRequestMessage,System.String)" />
    /// method.
    /// </summary>
    string GetAccessToken(HttpRequestMessage request);
}
