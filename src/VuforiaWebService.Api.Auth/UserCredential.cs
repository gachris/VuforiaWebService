using System.Security.Cryptography;
using System.Text;
using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Logger;

namespace VuforiaWebService.Api.Auth;

/// <summary>
/// Represents user credentials used for authentication in the Vuforia web service.
/// Implements various interfaces for handling access methods and HTTP requests.
/// </summary>
public class UserCredential : ICredential, IConfigurableHttpClientInitializer, IHttpExecuteInterceptor, IHttpUnsuccessfulResponseHandler
{
    /// <summary>
    /// Logger instance for logging actions related to user credentials.
    /// </summary>
    protected static readonly ILogger Logger = ApplicationContext.Logger.ForType<UserCredential>();

    private string _accessToken;

    /// <summary>
    /// Gets the access method used for authentication.
    /// </summary>
    public IAccessMethod AccessMethod { get; }

    /// <summary>
    /// Gets the current access token used for authentication.
    /// </summary>
    public string AccessToken => _accessToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserCredential"/> class.
    /// </summary>
    public UserCredential()
    {
        AccessMethod = new VuforiaWebServiceAuthentication.AuthorizationHeaderAccessMethod();
    }

    /// <summary>
    /// Intercepts the HTTP request to include the access token in the authorization header.
    /// If the access token is expired or nearing expiration, it may attempt to refresh the token.
    /// </summary>
    /// <param name="request">The HTTP request message to modify.</param>
    /// <param name="taskCancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InterceptAsync(HttpRequestMessage request, CancellationToken taskCancellationToken)
    {
        AccessMethod.Intercept(request, AccessToken);
        await Task.Delay(0);
    }

    /// <summary>
    /// Initializes the HTTP client by adding interceptors for request execution and unsuccessful responses.
    /// </summary>
    /// <param name="httpClient">The configurable HTTP client to initialize.</param>
    public void Initialize(ConfigurableHttpClient httpClient)
    {
        httpClient.MessageHandler.AddExecuteInterceptor(this);
        httpClient.MessageHandler.AddUnsuccessfulResponseHandler(this);
    }

    /// <summary>
    /// Generates an access token based on the provided parameters using HMAC-SHA1 hashing.
    /// </summary>
    /// <param name="clientService">The client service used for serialization and making requests.</param>
    /// <param name="keys">The database access keys used for authentication.</param>
    /// <param name="httpMethod">The HTTP method of the request (e.g., GET, POST).</param>
    /// <param name="body">The body of the request to be serialized.</param>
    /// <param name="requestPath">The path of the request being made.</param>
    public void GenerateAccessToken(IClientService clientService, ServerAccessKeys keys, string httpMethod, object body, string requestPath)
    {
        var requestBody = string.Empty;
        var contentType = "application/" + clientService.Serializer.Format;

        using var sha1 = new HMACSHA1(Encoding.ASCII.GetBytes(keys.SecretKey));

        if (body != null)
            requestBody = clientService.SerializeObject(body);

        var sha1Bytes = Encoding.ASCII.GetBytes($"{httpMethod}\n{CalculateMD5Hash(requestBody).ToLower()}\n{contentType}\n{string.Format("{0:r}", DateTime.Now.ToUniversalTime())}\n{requestPath}");

        using var stream = new MemoryStream(sha1Bytes);
        var signature = Convert.ToBase64String(sha1.ComputeHash(stream));

        _accessToken = $"{keys.AccessKey}:{signature}";
    }

    /// <summary>
    /// Calculates the MD5 hash of a given request body.
    /// </summary>
    /// <param name="requestBody">The request body to hash.</param>
    /// <returns>The MD5 hash as a hexadecimal string.</returns>
    private static string CalculateMD5Hash(string requestBody)
    {
        var sb = new StringBuilder();
        var inputBytes = Encoding.ASCII.GetBytes(requestBody);

        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(inputBytes);

        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Handles unsuccessful HTTP responses.
    /// </summary>
    /// <param name="args">Arguments containing details about the response.</param>
    /// <returns>A task representing the asynchronous operation, returning true if handled.</returns>
    public async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
    {
        await Task.Delay(0);
        return false;
    }
}