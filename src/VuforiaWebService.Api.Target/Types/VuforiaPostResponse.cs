using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Response;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for a request to create a new target in the Vuforia web service.
/// Inherits from the base response class <see cref="VuforiaBaseResponse"/>.
/// </summary>
public class VuforiaPostResponse : VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the newly created target.
    /// </summary>
    [JsonProperty("target_id")]
    public string TargetId { get; set; }
}