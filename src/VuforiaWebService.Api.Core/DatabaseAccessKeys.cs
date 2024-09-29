namespace VuforiaWebService.Api.Core;

/// <summary>
/// Represents the access keys required to authenticate with the Vuforia database service.
/// This class encapsulates the access key and secret key used for API authentication.
/// </summary>
public class DatabaseAccessKeys
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
    /// Initializes a new instance of the <see cref="DatabaseAccessKeys"/> class.
    /// </summary>
    /// <param name="accessKey">The access key used for authentication.</param>
    /// <param name="secretKey">The secret key used for authentication.</param>
    public DatabaseAccessKeys(string accessKey, string secretKey)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
    }
}
