using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Types;

namespace VuforiaWebService.Api.Target.Types;

public class VuforiaGetDatabaseSummaryReportResponse : VuforiaBaseResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("active_images")]
    public int ActiveImages { get; set; }

    [JsonProperty("inactive_images")]
    public int InactiveImages { get; set; }

    [JsonProperty("failed_images")]
    public int FailedImages { get; set; }
}