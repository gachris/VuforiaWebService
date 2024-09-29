using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Response;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for a request to retrieve all targets from the Vuforia web service.
/// Inherits from the base response class <see cref="VuforiaBaseResponse"/>.
/// </summary>
public class VuforiaGetAllResponse : VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets an array of target IDs returned by the Vuforia web service.
    /// </summary>
    [JsonProperty("results")]
    public string[] Results { get; set; }
}