using Newtonsoft.Json;
using VuforiaWebService.Api.Core.Utils;

namespace VuforiaWebService.Api.Core;

/// <summary>
/// Custom JSON converter for handling DateTime serialization in RFC 3339 format.
/// This converter is intended for use with the Newtonsoft.Json library.
/// </summary>
public class RFC3339DateTimeConverter : JsonConverter
{
    /// <summary>
    /// Gets a value indicating whether this converter can read JSON data.
    /// Returns false since reading is not implemented for this converter.
    /// </summary>
    public override bool CanRead => false;

    /// <summary>
    /// Reads the JSON representation of the object. 
    /// This method is not implemented because reading is not supported.
    /// </summary>
    /// <param name="reader">The JSON reader to read from.</param>
    /// <param name="objectType">The type of the object to convert.</param>
    /// <param name="existingValue">The existing value of the object being read.</param>
    /// <param name="serializer">The serializer being used.</param>
    /// <returns>Throws NotImplementedException since CanRead is false.</returns>
    /// <exception cref="NotImplementedException">Always thrown when called.</exception>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        => throw new NotImplementedException("Unnecessary because CanRead is false.");

    /// <summary>
    /// Determines whether this converter can convert the specified object type.
    /// The converter can handle both <see cref="DateTime"/>.
    /// </summary>
    /// <param name="objectType">The type of the object to convert.</param>
    /// <returns>True if the type is DateTime or DateTime?; otherwise, false.</returns>
    public override bool CanConvert(Type objectType)
        => objectType == typeof(DateTime) || objectType == typeof(DateTime?);

    /// <summary>
    /// Writes the JSON representation of the DateTime value.
    /// The DateTime is serialized in RFC 3339 format using the utility method <see cref="Utilities.ConvertToRFC3339"/>.
    /// </summary>
    /// <param name="writer">The JSON writer to write to.</param>
    /// <param name="value">The DateTime value to convert.</param>
    /// <param name="serializer">The serializer being used.</param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
            return; // Do not write anything if the value is null

        DateTime date = (DateTime)value; // Cast the value to DateTime
        serializer.Serialize(writer, Utilities.ConvertToRFC3339(date)); // Serialize the date in RFC 3339 format
    }
}