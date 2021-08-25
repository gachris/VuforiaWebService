using Newtonsoft.Json;
using System;

namespace VuforiaWebService.Api.Core.Services
{
    public class RFC3339DateTimeConverter : JsonConverter
    {
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false.");
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType != typeof(DateTime))
                return objectType == typeof(DateTime?);
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                return;
            DateTime date = (DateTime)value;
            serializer.Serialize(writer, Utilities.ConvertToRFC3339(date));
        }
    }
}
