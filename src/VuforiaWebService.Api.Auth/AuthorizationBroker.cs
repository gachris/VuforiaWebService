using System.Net;

namespace VuforiaWebService.Api.Auth;

public class AuthorizationBroker
{
    /// <summary>The core logic for asynchronously authorizing the specified user.</summary>
    /// <param name="networdCredentials">
    /// The netword credentials which indicate the VuforiaPortal API access your application is requesting.
    /// </param>
    /// <returns>User credential.</returns>
    public static UserCredential AuthorizeAsync(NetworkCredential networdCredentials) => new UserCredential(networdCredentials);
}
