using System.Net;
using System.Threading;

namespace VuforiaWebService.Api.Auth
{
    public class AuthorizationBroker
    {
        /// <summary>The folder which is used by the <see cref="T:VuforiaPortal.Apis.Util.Store.FileDataStore" />.</summary>
        /// <remarks>
        /// The reason that this is not 'private const' is that a user can change it and store the credentials in a
        /// different location.
        /// </remarks>
        public static string Folder = "VuforiaPortal.Apis.Auth";

        /// <summary>The core logic for asynchronously authorizing the specified user.</summary>
        /// <param name="initializer">The authorization code initializer.</param>
        /// <param name="networdCredentials">
        /// The netword credentials which indicate the VuforiaPortal API access your application is requesting.
        /// </param>
        /// <param name="user">The user to authorize.</param>
        /// <param name="taskCancellationToken">Cancellation token to cancel an operation.</param>
        /// <param name="dataStore">The data store, if not specified a file data store will be used.</param>
        /// <returns>User credential.</returns>
        public static UserCredential AuthorizeAsync(NetworkCredential networdCredentials)
        {
            return new UserCredential(networdCredentials);
        }
    }
}
