using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Response;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for a request to retrieve a summary report of a Vuforia database.
/// Inherits from the base response class <see cref="VuforiaBaseResponse"/>.
/// </summary>
public class VuforiaGetDatabaseSummaryReportResponse : VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets the name of the Vuforia database.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the number of active images in the database.
    /// </summary>
    [JsonProperty("active_images")]
    public int ActiveImages { get; set; }

    /// <summary>
    /// Gets or sets the number of inactive images in the database.
    /// </summary>
    [JsonProperty("inactive_images")]
    public int InactiveImages { get; set; }

    /// <summary>
    /// Gets or sets the number of images in the database that failed processing.
    /// </summary>
    [JsonProperty("failed_images")]
    public int FailedImages { get; set; }
}