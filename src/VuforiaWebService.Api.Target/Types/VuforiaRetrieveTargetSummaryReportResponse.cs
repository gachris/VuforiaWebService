using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Response;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for retrieving a summary report of a specific target from the Vuforia web service.
/// Inherits from the base response class <see cref="VuforiaBaseResponse"/>.
/// </summary>
public class VuforiaRetrieveTargetSummaryReportResponse : VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets the name of the Vuforia database containing the target.
    /// </summary>
    [JsonProperty("database_name")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets the name of the target for which the report is generated.
    /// </summary>
    [JsonProperty("target_name")]
    public string TargetName { get; set; }

    /// <summary>
    /// Gets or sets the date when the target was uploaded.
    /// </summary>
    [JsonProperty("upload_date")]
    public string UploadDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the target is currently active.
    /// </summary>
    [JsonProperty("active_flag")]
    public bool ActiveFlag { get; set; }

    /// <summary>
    /// Gets or sets the status of the target retrieval process.
    /// </summary>
    [JsonProperty("status")]
    public StatusEnum Status { get; set; }

    /// <summary>
    /// Gets or sets the tracking rating of the target, usually between 0 and 5.
    /// </summary>
    [JsonProperty("tracking_rating")]
    public int TrackingRating { get; set; }

    /// <summary>
    /// Gets or sets the recognition rating of the target as a string.
    /// </summary>
    [JsonProperty("reco_rating")]
    public string RecoRating { get; set; }

    /// <summary>
    /// Gets or sets the total number of successful recognitions of the target.
    /// </summary>
    [JsonProperty("total_recos")]
    public int TotalRecos { get; set; }

    /// <summary>
    /// Gets or sets the number of successful recognitions of the target for the current month.
    /// </summary>
    [JsonProperty("current_month_recos")]
    public int CurrentMonthRecos { get; set; }

    /// <summary>
    /// Gets or sets the number of successful recognitions of the target for the previous month.
    /// </summary>
    [JsonProperty("previous_month_recos")]
    public int PreviousMonthRecos { get; set; }

    /// <summary>
    /// Defines the status of the target retrieval process.
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// The request is still processing.
        /// </summary>
        Processing,

        /// <summary>
        /// The request was successful.
        /// </summary>
        Success,

        /// <summary>
        /// The request has failed.
        /// </summary>
        Failure
    }
}