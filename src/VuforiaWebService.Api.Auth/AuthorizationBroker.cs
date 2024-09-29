using System.Net;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// Represents an Authorization Broker for handling Basic Authentication.
/// </summary>
public class AuthorizationBroker
{
    /// <summary>
    /// Authorizes a user asynchronously using Basic Authentication.
    /// </summary>
    /// <param name="networkCredentials">The user's network credentials (username and password).</param>
    /// <returns>The user's credentials upon successful authorization.</returns>
    public static UserCredential AuthorizeAsync(NetworkCredential networkCredentials) => new UserCredential(networkCredentials);
}
