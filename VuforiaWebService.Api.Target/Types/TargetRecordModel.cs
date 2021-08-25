using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types
{
    public class TargetRecordModel
    {
        [JsonProperty("target_id")]
        public string TargetId { get; set; }

        [JsonProperty("active_flag")]
        public string ActiveFlag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("tracking_rating")]
        public int TrackingRating { get; set; }

        [JsonProperty("reco_rating")]
        public string RecoRating { get; set; }
    }
}