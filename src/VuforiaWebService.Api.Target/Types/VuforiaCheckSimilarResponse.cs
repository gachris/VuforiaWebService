using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for a check similar targets request to the Vuforia web service.
/// </summary>
public class VuforiaCheckSimilarResponse
{
    /// <summary>
    /// Gets or sets an array of similar target IDs returned by the Vuforia web service.
    /// </summary>
    [JsonProperty("similar_targets")]
    public string[] SimilarTargets { get; set; }
}