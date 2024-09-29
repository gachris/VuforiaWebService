using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Target.Resources;

namespace VuforiaWebService.Api.Target.Services;

/// <summary>
/// Represents the service to interact with the vuforia web service.
/// </summary>
public class TargetService : BaseClientService
{
    /// <summary>
    /// The vuforia web service version.
    /// </summary>
    public const string Version = "v1";

    /// <summary>
    /// Initializes a new instance of the <see cref="TargetService"/> class.
    /// </summary>
    /// <param name="initializer">The initializer to use for the service.</param>
    public TargetService(Initializer initializer) : base(initializer)
    {
        TargetList = new TargetListResource(this);
    }

    /// <inheritdoc/>
    public override string Name => "target";

    /// <inheritdoc/>
    public override string BaseUri => "https://vws.vuforia.com/";

    /// <summary>
    /// Gets the <see cref="TargetListResource"/>.
    /// </summary>
    public virtual TargetListResource TargetList { get; }
}