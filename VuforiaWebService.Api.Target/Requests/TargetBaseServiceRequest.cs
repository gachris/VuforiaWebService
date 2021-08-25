using VuforiaWebService.Api.Core.Services;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Request
{
    public abstract class TargetBaseServiceRequest<TResponse> : ClientServiceRequest<TResponse>
    {
        ///<summary>Constructs a new TargetBaseServiceRequest instance.</summary>
        protected TargetBaseServiceRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys)
        {
        }

        /// <summary>Initializes Target parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();
        }
    }
}
