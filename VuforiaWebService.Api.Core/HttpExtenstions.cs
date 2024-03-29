﻿using System.Net;
using System.Net.Http;

namespace VuforiaWebService.Api.Core;

/// <summary>
/// Extension methods to <see cref="T:System.Net.Http.HttpRequestMessage" /> and
/// <see cref="T:System.Net.Http.HttpResponseMessage" />.
/// </summary>
public static class HttpExtenstions
{
    /// <summary>Returns <c>true</c> if the response contains one of the redirect status codes.</summary>
    internal static bool IsRedirectStatusCode(this HttpResponseMessage message)
    {
        switch (message.StatusCode)
        {
            case HttpStatusCode.Moved:
            case HttpStatusCode.Found:
            case HttpStatusCode.RedirectMethod:
            case HttpStatusCode.RedirectKeepVerb:
                return true;
            default:
                return false;
        }
    }

    /// <summary>A VuforiaPortal.Apis utility method for setting an empty HTTP content.</summary>
    public static HttpContent SetEmptyContent(this HttpRequestMessage request)
    {
        request.Content = new ByteArrayContent(new byte[0]);
        request.Content.Headers.ContentLength = new long?(0L);
        return request.Content;
    }
}
