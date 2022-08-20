using System.Collections.Generic;
using VuforiaWebService.Api.Core.Services;

namespace VuforiaWebService.Api.Target;

public class TargetService : BaseClientService
{
    public const string Version = "v1";

    public TargetService(Initializer initializer) : base(initializer)
    {
        TargetList = new TargetListResource(this);
    }

    public override string Name => "target";

    public override string BaseUri => "https://vws.vuforia.com/";

    public override string BasePath { get; }

    public override IList<string> Features => new string[0];

    /// <summary>Gets the TargetList resource.</summary>
    public virtual TargetListResource TargetList { get; }
}