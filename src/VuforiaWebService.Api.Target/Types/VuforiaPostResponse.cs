using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Types;

public class VuforiaPostResponse : VuforiaBaseResponse
{
    [JsonProperty("target_id")]
    public string TargetId { get; set; }
}