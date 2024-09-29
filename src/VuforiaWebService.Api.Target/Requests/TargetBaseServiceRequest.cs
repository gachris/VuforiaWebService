using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Request;

namespace VuforiaWebService.Api.Target.Requests;

/// <summary>
/// An abstract base class for service requests used in the vuforia web service.
/// </summary>
public abstract class TargetBaseServiceRequest<TResponse> : ClientServiceRequest<TResponse>
{
    /// <summary>
    /// Constructs a new instance of the <see cref="TargetBaseServiceRequest{TResponse}"/> class.
    /// </summary>
    /// <param name="service">The client service instance used to make the request.</param>
    /// <param name="keys">The database access keys required for authorization.</param>
    protected TargetBaseServiceRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys)
    {
    }
}
