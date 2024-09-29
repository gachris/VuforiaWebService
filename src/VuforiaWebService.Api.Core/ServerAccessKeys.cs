namespace VuforiaWebService.Api.Core;

/// <summary>
/// Represents the access keys required to authenticate with the Vuforia database service.
/// This class encapsulates the access key and secret key used for API authentication.
/// </summary>
public class ServerAccessKeys
{
    /// <summary>
    /// Gets the access key used for authentication with the database service.
    /// </summary>
    public string AccessKey { get; }

    /// <summary>
    /// Gets the secret key used for authentication with the database service.
    /// </summary>
    public string SecretKey { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerAccessKeys"/> class.
    /// </summary>
    /// <param name="accessKey">The access key used for authentication.</param>
    /// <param name="secretKey">The secret key used for authentication.</param>
    public ServerAccessKeys(string accessKey, string secretKey)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
    }
}
