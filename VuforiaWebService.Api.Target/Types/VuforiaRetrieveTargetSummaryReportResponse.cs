using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Types
{
    public class VuforiaRetrieveTargetSummaryReportResponse : VuforiaBaseResponse
    {
        [JsonProperty("database_name")]
        public string DatabaseName { get; set; }

        [JsonProperty("target_name")]
        public string TargetName { get; set; }

        [JsonProperty("upload_date")]
        public string UploadDate { get; set; }

        [JsonProperty("active_flag")]
        public bool ActiveFlag { get; set; }

        [JsonProperty("status")]
        public StatusEnum Status { get; set; }

        [JsonProperty("tracking_rating")]
        public int TrackingRating { get; set; }

        [JsonProperty("reco_rating")]
        public string RecoRating { get; set; }

        [JsonProperty("total_recos")]
        public int TotalRecos { get; set; }

        [JsonProperty("current_month_recos")]
        public int CurrentMonthRecos { get; set; }

        [JsonProperty("previous_month_recos")]
        public int PreviousMonthRecos { get; set; }

        public enum StatusEnum
        {
            Processing, 
            Success, 
            Failure
        }
    }
}