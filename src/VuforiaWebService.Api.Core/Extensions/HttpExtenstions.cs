namespace VuforiaWebService.Api.Core.Extensions;

/// <summary>
/// Extension methods to <see cref="T:System.Net.Http.HttpRequestMessage" /> and
/// <see cref="T:System.Net.Http.HttpResponseMessage" />.
/// </summary>
public static class HttpExtenstions
{
    /// <summary>Returns <c>true</c> if the response contains one of the redirect status codes.</summary>
    internal static bool IsRedirectStatusCode(this HttpResponseMessage message)
    {
        return message.StatusCode switch
        {
            HttpStatusCode.Moved
 /* Unmerged change from project 'VuforiaWebService.Api.Core (netstandard2.0)'
 Before:
             case HttpStatusCode.Found:
             case HttpStatusCode.RedirectMethod:
 After:
             case HttpStatusCode.Found HttpStatusCode.RedirectMethod:
 */
 or HttpStatusCode.Found or or HttpStatusCode.RedirectMethod or HttpStatusCode.RedirectKeepVerb => true,
            _ => false,
        };
    }

    /// <summary>A VuforiaPortal.Apis utility method for setting an empty HTTP content.</summary>
    public static HttpContent SetEmptyContent(this HttpRequestMessage request)
    {
        request.Content = new ByteArrayContent(new byte[0]);
        request.Content.Headers.ContentLength = new long?(0L);
        return request.Content;
    }
}
