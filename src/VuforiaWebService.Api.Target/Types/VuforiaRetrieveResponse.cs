using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Types;

public class VuforiaRetrieveResponse : VuforiaBaseResponse
{
    [JsonProperty("target_record")]
    public TargetRecordModel TargetRecord { get; set; }

    [JsonProperty("status")]
    public StatusEnum Status { get; set; }

    public enum StatusEnum
    {
        Processing,
        Success,
        Failed
    }
}