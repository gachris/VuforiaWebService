using System.Text;

namespace VuforiaWebService.Api.Core.Extensions;

/// <summary>Extension methods to <see cref="T:System.Net.Http.HttpRequestMessage" />.</summary>
internal static class HttpRequestMessageExtenstions
{
    /// <summary>
    /// Sets the content of the request by the given body and the the required GZip configuration.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="service">The service.</param>
    /// <param name="body">The body of the future request. If <c>null</c> do nothing.</param>
    internal static void SetRequestSerailizedContent(this HttpRequestMessage request, IClientService service, object body)
    {
        string content = "";
        string mediaType = "application/" + service.Serializer.Format;
        if (body != null)
        {
            content = service.SerializeObject(body);
        }
        HttpContent httpContent;
        httpContent = new StringContent(content, Encoding.UTF8, mediaType);
        request.Content = httpContent;
    }
}
