using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Logger;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Auth;

public class UserCredential : ICredential, IConfigurableHttpClientInitializer, IHttpExecuteInterceptor, IHttpUnsuccessfulResponseHandler
{
    protected static readonly ILogger Logger = ApplicationContext.Logger.ForType<UserCredential>();
    private string accessToken;
    private readonly NetworkCredential networkCredential;

    public IAccessMethod AccessMethod { get; }

    /// <summary>Gets the user identity.</summary>
    public NetworkCredential NetworkCredential => networkCredential;

    /// <summary>Gets the user identity.</summary>
    public string AccessToken => accessToken;

    /// <summary>Constructs a new credential instance.</summary>
    /// <param name="flow">Authorization code flow.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="token">An initial token for the user.</param>
    public UserCredential(NetworkCredential networkCredential)
    {
        AccessMethod = new VuforiaWebServiceAuthentication.AuthorizationHeaderAccessMethod();
        this.networkCredential = networkCredential;
    }

    /// <summary>
    /// Default implementation is to try to refresh the access token if there is no access token or if we are 1
    /// minute away from expiration. If token server is unavailable, it will try to use the access token even if
    /// has expired. If successful, it will call <see cref="M:VuforiaPortal.Apis.Auth.OAuth2.IAccessMethod.Intercept(System.Net.Http.HttpRequestMessage,System.String)" />.
    /// </summary>
    public async Task InterceptAsync(HttpRequestMessage request, CancellationToken taskCancellationToken)
    {
        AccessMethod.Intercept(request, AccessToken);
        await Task.Delay(0);
    }

    public void Initialize(ConfigurableHttpClient httpClient)
    {
        httpClient.MessageHandler.AddExecuteInterceptor(this);
        httpClient.MessageHandler.AddUnsuccessfulResponseHandler(this);
    }

    public void GenerateAccessToken(IClientService clientService, DatabaseAccessKeys keys, string httpMethod, object body, string requestPath)
    {
        using HMACSHA1 sha1 = new HMACSHA1(Encoding.ASCII.GetBytes(keys.SecretKey));
        string contentType = "application/" + clientService.Serializer.Format;
        var requestBody = "";
        if (body != null)
            requestBody = clientService.SerializeObject(body);
        byte[] sha1Bytes = Encoding.ASCII.GetBytes($"{httpMethod}\n{CalculateMD5Hash(requestBody).ToLower()}\n{contentType}\n{string.Format("{0:r}", DateTime.Now.ToUniversalTime())}\n{requestPath}");
        MemoryStream stream = new MemoryStream(sha1Bytes);
        var signature = Convert.ToBase64String(sha1.ComputeHash(stream));
        accessToken = $"{keys.AccessKey}:{signature}";
    }

    private static string CalculateMD5Hash(string requestBody)
    {
        using MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(requestBody);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));
        return sb.ToString();
    }

    public async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
    {
        await Task.Delay(0);
        return false;
    }
}