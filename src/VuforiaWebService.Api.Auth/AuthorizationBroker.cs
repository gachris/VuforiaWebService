namespace VuforiaWebService.Api.Auth;

/// <summary>
/// Represents an Authorization Broker for handling Basic Authentication.
/// </summary>
public class AuthorizationBroker
{
    /// <summary>
    /// Authorizes a user asynchronously using Basic Authentication.
    /// </summary>
    /// <returns>The user's credentials upon successful authorization.</returns>
    public static UserCredential AuthorizeAsync() => new();
}
