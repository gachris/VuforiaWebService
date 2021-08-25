using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types
{
    public class PostTrackableRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("width")]
        public float Width { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("active_flag")]
        public bool ActiveFlag { get; set; }

        [JsonProperty("application_metadata")]
        public string ApplicationMetadata { get; set; }
    }
}