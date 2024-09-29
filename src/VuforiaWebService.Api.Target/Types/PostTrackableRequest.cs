using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents a request to post a trackable object to the Vuforia web service.
/// </summary>
public class PostTrackableRequest
{
    /// <summary>
    /// Gets or sets the name of the trackable object.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the width of the trackable object in scene units.
    /// </summary>
    [JsonProperty("width")]
    public float Width { get; set; }

    /// <summary>
    /// Gets or sets the base64-encoded image data of the trackable object.
    /// </summary>
    [JsonProperty("image")]
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the trackable object is active.
    /// </summary>
    [JsonProperty("active_flag")]
    public bool? ActiveFlag { get; set; }

    /// <summary>
    /// Gets or sets the application-specific metadata associated with the trackable object.
    /// </summary>
    [JsonProperty("application_metadata")]
    public string ApplicationMetadata { get; set; }
}