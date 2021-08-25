using Newtonsoft.Json;
using System.ComponentModel;

namespace VuforiaWebService.Api.Core.Types
{
    public class VuforiaBaseResponse
    {
        [JsonProperty("result_code")]
        public ResultCodeEnum ResultCode { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        public enum ResultCodeEnum
        {
            [Description("Transaction succeeded")]
            Success   /*  OK (200)*/ 	,
            [Description("Target created (target POST response)")]
            TargetCreated   /*Created (201)*/ 	,
            [Description("Signature authentication failed")]
            AuthenticationFailure   /*Authentication failure (401)*/,
            [Description("Request timestamp outside allowed range")]
            RequestTimeTooSkewed    /*Forbidden (403)*/ 	,
            [Description("The corresponding target name already exists (target POST/PUT response)")]
            TargetNameExist     /*Forbidden (403)*/ 	,
            [Description("The maximum number of API calls for this database has been reached.")]
            RequestQuotaReached     /*Forbidden (403)*/ 	,
            [Description("The target is in the processing state and cannot be updated.")]
            TargetStatusProcessing  /*Forbidden (403)*/ 	,
            [Description("The request could not be completed because the target is not in the success state.")]
            TargetStatusNotSuccess  /*Forbidden (403)*/ 	,
            [Description("The maximum number of targets for this database has been reached.")]
            TargetQuotaReached  /*Forbidden (403)*/ 	,
            [Description("The request could not be completed because this database has been suspended.")]
            ProjectSuspended    /*Forbidden (403)*/ 	,
            [Description("The request could not be completed because this database is inactive.")]
            ProjectInactive     /*Forbidden (403)*/ 	,
            [Description("The request could not be completed because this database is not allowed to make API requests.")]
            ProjectHasNoApiAccess   /*Forbidden (403)*/ 	,
            [Description("The specified target ID does not exist (target PUT/GET/DELETE response)")]
            UnknownTarget   /*Not Found (404)*/ 	,
            [Description("Image corrupted or format not supported (target POST/PUT response)")]
            BadImage    /*Unprocessable Entity (422)*/ 	,
            [Description("Target metadata size exceeds maximum limit (target POST/PUT response)")]
            ImageTooLarge   /*Unprocessable Entity (422)*/ 	,
            [Description("Image size exceeds maximum limit (target POST/PUT response)")]
            MetadataTooLarge    /*Unprocessable Entity (422)*/ 	,
            [Description("Start date is after the end date")]
            DateRangeError  /*Unprocessable Entity (422)*/ 	,
            [Description("The request was invalid and could not be processed. Check the request headers and fields. | The server encountered an internal error; please retry the request")]
            Fail    /*Unprocessable Entity (422) | Internal Server Error (500)*/ 	,
        }
    }
}