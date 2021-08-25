using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Types
{
    public class VuforiaGetAllResponse : VuforiaBaseResponse
    {
        [JsonProperty("results")]
        public string[] Results { get; set; }
    }
}