using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the data model for a target record retrieved from the Vuforia web service.
/// </summary>
public class TargetRecordModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the target.
    /// </summary>
    [JsonProperty("target_id")]
    public string TargetId { get; set; }

    /// <summary>
    /// Gets or sets the status of whether the target is active (as a string).
    /// </summary>
    [JsonProperty("active_flag")]
    public string ActiveFlag { get; set; }

    /// <summary>
    /// Gets or sets the name of the target.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the width of the target in scene units.
    /// </summary>
    [JsonProperty("width")]
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the tracking rating of the target, typically between 0 and 5.
    /// </summary>
    [JsonProperty("tracking_rating")]
    public int TrackingRating { get; set; }

    /// <summary>
    /// Gets or sets the recognition rating of the target as a string value.
    /// </summary>
    [JsonProperty("reco_rating")]
    public string RecoRating { get; set; }
}
