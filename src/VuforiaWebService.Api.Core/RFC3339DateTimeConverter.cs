using Newtonsoft.Json;
using System;
using VuforiaWebService.Api.Core.Utils;

namespace VuforiaWebService.Api.Core;

public class RFC3339DateTimeConverter : JsonConverter
{
    public override bool CanRead => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => throw new NotImplementedException("Unnecessary because CanRead is false.");

    public override bool CanConvert(Type objectType) => objectType != typeof(DateTime) ? objectType == typeof(DateTime?) : true;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
            return;
        DateTime date = (DateTime)value;
        serializer.Serialize(writer, Utilities.ConvertToRFC3339(date));
    }
}
