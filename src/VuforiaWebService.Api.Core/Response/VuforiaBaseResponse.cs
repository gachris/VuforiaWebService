using System.ComponentModel;
using Newtonsoft.Json;

namespace VuforiaWebService.Api.Core.Response;

/// <summary>
/// Represents the base response for Vuforia API calls, containing the result code and transaction ID.
/// </summary>
public class VuforiaBaseResponse
{
    /// <summary>
    /// Gets or sets the result code indicating the status of the API request.
    /// </summary>
    [JsonProperty("result_code")]
    public ResultCodeEnum ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the transaction associated with the API request.
    /// </summary>
    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; }

    /// <summary>
    /// Enumeration representing the various result codes that can be returned from Vuforia API responses.
    /// Each value corresponds to a specific status and includes a description for clarity.
    /// </summary>
    public enum ResultCodeEnum
    {
        /// <summary>
        /// Transaction succeeded (HTTP status code 200).
        /// </summary>
        [Description("Transaction succeeded")]
        Success,  // OK (200)

        /// <summary>
        /// Target created successfully (HTTP status code 201).
        /// </summary>
        [Description("Target created (target POST response)")]
        TargetCreated,  // Created (201)

        /// <summary>
        /// Signature authentication failed (HTTP status code 401).
        /// </summary>
        [Description("Signature authentication failed")]
        AuthenticationFailure,  // Authentication failure (401)

        /// <summary>
        /// Request timestamp is outside the allowed range (HTTP status code 403).
        /// </summary>
        [Description("Request timestamp outside allowed range")]
        RequestTimeTooSkewed,  // Forbidden (403)

        /// <summary>
        /// The corresponding target name already exists (HTTP status code 403).
        /// </summary>
        [Description("The corresponding target name already exists (target POST/PUT response)")]
        TargetNameExist,  // Forbidden (403)

        /// <summary>
        /// The maximum number of API calls for this database has been reached (HTTP status code 403).
        /// </summary>
        [Description("The maximum number of API calls for this database has been reached.")]
        RequestQuotaReached,  // Forbidden (403)

        /// <summary>
        /// The target is in the processing state and cannot be updated (HTTP status code 403).
        /// </summary>
        [Description("The target is in the processing state and cannot be updated.")]
        TargetStatusProcessing,  // Forbidden (403)

        /// <summary>
        /// The request could not be completed because the target is not in the success state (HTTP status code 403).
        /// </summary>
        [Description("The request could not be completed because the target is not in the success state.")]
        TargetStatusNotSuccess,  // Forbidden (403)

        /// <summary>
        /// The maximum number of targets for this database has been reached (HTTP status code 403).
        /// </summary>
        [Description("The maximum number of targets for this database has been reached.")]
        TargetQuotaReached,  // Forbidden (403)

        /// <summary>
        /// The request could not be completed because this database has been suspended (HTTP status code 403).
        /// </summary>
        [Description("The request could not be completed because this database has been suspended.")]
        ProjectSuspended,  // Forbidden (403)

        /// <summary>
        /// The request could not be completed because this database is inactive (HTTP status code 403).
        /// </summary>
        [Description("The request could not be completed because this database is inactive.")]
        ProjectInactive,  // Forbidden (403)

        /// <summary>
        /// The request could not be completed because this database is not allowed to make API requests (HTTP status code 403).
        /// </summary>
        [Description("The request could not be completed because this database is not allowed to make API requests.")]
        ProjectHasNoApiAccess,  // Forbidden (403)

        /// <summary>
        /// The specified target ID does not exist (HTTP status code 404).
        /// </summary>
        [Description("The specified target ID does not exist (target PUT/GET/DELETE response)")]
        UnknownTarget,  // Not Found (404)

        /// <summary>
        /// Image is corrupted or format not supported (HTTP status code 422).
        /// </summary>
        [Description("Image corrupted or format not supported (target POST/PUT response)")]
        BadImage,  // Unprocessable Entity (422)

        /// <summary>
        /// Target metadata size exceeds maximum limit (HTTP status code 422).
        /// </summary>
        [Description("Target metadata size exceeds maximum limit (target POST/PUT response)")]
        ImageTooLarge,  // Unprocessable Entity (422)

        /// <summary>
        /// Image size exceeds maximum limit (HTTP status code 422).
        /// </summary>
        [Description("Image size exceeds maximum limit (target POST/PUT response)")]
        MetadataTooLarge,  // Unprocessable Entity (422)

        /// <summary>
        /// Start date is after the end date (HTTP status code 422).
        /// </summary>
        [Description("Start date is after the end date")]
        DateRangeError,  // Unprocessable Entity (422)

        /// <summary>
        /// The request was invalid and could not be processed, or the server encountered an internal error (HTTP status codes 422 or 500).
        /// </summary>
        [Description("The request was invalid and could not be processed. Check the request headers and fields. | The server encountered an internal error; please retry the request")]
        Fail  // Unprocessable Entity (422) | Internal Server Error (500)
    }
}
