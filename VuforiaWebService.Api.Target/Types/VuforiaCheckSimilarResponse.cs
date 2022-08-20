using Newtonsoft.Json;

namespace VuforiaWebService.Api.Target.Types;

public class VuforiaCheckSimilarResponse
{
    [JsonProperty("similar_targets")]
    public string[] SimilarTargets { get; set; }
}