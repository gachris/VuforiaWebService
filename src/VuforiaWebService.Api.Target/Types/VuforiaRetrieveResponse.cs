using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Response;

namespace VuforiaWebService.Api.Target.Types;

/// <summary>
/// Represents the response model for retrieving a target's details from the Vuforia web service.
/// Inherits from the base response class <see cref="VuforiaBaseResponse"/>.
/// </summary>
public class VuforiaRetrieveResponse : VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets the target record data, which contains detailed information about the target.
    /// </summary>
    [JsonProperty("target_record")]
    public TargetRecordModel TargetRecord { get; set; }

    /// <summary>
    /// Gets or sets the status of the retrieve request, indicating the current state of the operation.
    /// </summary>
    [JsonProperty("status")]
    public StatusEnum Status { get; set; }

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
        Failed
    }
}