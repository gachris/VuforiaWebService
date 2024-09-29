using System;
using System.Net;
using VuforiaWebService.Api.Core.Extensions;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Core;

/// <summary>Represents an exception thrown by an API Service.</summary>
public class VuforiaPortalApiException : Exception
{
    private readonly string serviceName;

    /// <summary>Gets the service name which related to this exception.</summary>
    public string ServiceName => serviceName;

    /// <summary>Creates an API Service exception.</summary>
    public VuforiaPortalApiException(string serviceName, string message, Exception inner)
      : base(message, inner)
    {
        serviceName.ThrowIfNull(nameof(serviceName));
        this.serviceName = serviceName;
    }

    /// <summary>Creates an API Service exception.</summary>
    public VuforiaPortalApiException(string serviceName, string message)
      : this(serviceName, message, null)
    {
    }

    /// <summary>The Error which was returned from the server, or <c>null</c> if unavailable.</summary>
    public VuforiaErrorResponse Error { get; set; }

    /// <summary>The HTTP status code which was returned along with this error, or 0 if unavailable.</summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    public override string ToString() => string.Format("The service {1} has thrown an exception: {0}", base.ToString(), serviceName);
}
