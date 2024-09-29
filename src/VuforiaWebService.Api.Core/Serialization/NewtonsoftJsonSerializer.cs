using Newtonsoft.Json;

namespace VuforiaWebService.Api.Core.Serialization;

/// <summary>
/// Provides methods for serializing and deserializing objects using the Newtonsoft.Json library.
/// This class implements the ISerializer interface and follows the Singleton design pattern.
/// </summary>
public class NewtonsoftJsonSerializer : ISerializer
{
    #region Fields/Consts

    private static readonly JsonSerializer _newtonsoftSerializer;
    private static readonly NewtonsoftJsonSerializer _instance = new();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the <see cref="NewtonsoftJsonSerializer"/>.
    /// This ensures that there is only one instance of this serializer used throughout the application.
    /// </summary>
    public static NewtonsoftJsonSerializer Instance => _instance;

    /// <summary>
    /// Gets the format of the serializer, which is "json".
    /// </summary>
    public string Format => "json";

    #endregion

    /// <summary>
    /// Initializes the static members of the <see cref="NewtonsoftJsonSerializer"/> class.
    /// Sets up the Newtonsoft.Json serializer with specific settings.
    /// </summary>
    static NewtonsoftJsonSerializer()
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore // Ignore null values during serialization
        };

        settings.Converters.Add(new RFC3339DateTimeConverter()); // Add custom date-time converter

        _newtonsoftSerializer = JsonSerializer.Create(settings);
    }

    /// <summary>
    /// Serializes the specified object to the provided stream.
    /// If the object is null, an empty string is serialized.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="target">The stream to write the serialized object to.</param>
    public void Serialize(object obj, Stream target)
    {
        obj ??= string.Empty; // Default to empty string if obj is null

        using var streamWriter = new StreamWriter(target);
        _newtonsoftSerializer.Serialize(streamWriter, obj);
    }

    /// <summary>
    /// Serializes the specified object to a JSON string.
    /// If the object is null, an empty string is returned.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>The serialized JSON string.</returns>
    public string Serialize(object obj)
    {
        obj ??= string.Empty; // Default to empty string if obj is null

        using var textWriter = new StringWriter();
        _newtonsoftSerializer.Serialize(textWriter, obj);

        return textWriter.ToString();
    }

    /// <summary>
    /// Deserializes the specified JSON string into an object of the specified type.
    /// Returns the default value of the type if the input is null or empty.
    /// </summary>
    /// <typeparam name="T">The type to deserialize into.</typeparam>
    /// <param name="input">The JSON string to deserialize.</param>
    /// <returns>The deserialized object of type T, or the default value if input is null or empty.</returns>
    public T Deserialize<T>(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return default; // Return default value for type T if input is null or empty
        }

        using var stringReader = new StringReader(input);
        return (T)_newtonsoftSerializer.Deserialize(stringReader, typeof(T));
    }

    /// <summary>
    /// Deserializes the specified JSON string into an object of the specified type.
    /// Returns null if the input is null or empty.
    /// </summary>
    /// <param name="input">The JSON string to deserialize.</param>
    /// <param name="type">The type to deserialize into.</param>
    /// <returns>The deserialized object of the specified type, or null if input is null or empty.</returns>
    public object Deserialize(string input, Type type)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null; // Return null if input is null or empty
        }

        using var stringReader = new StringReader(input);
        return _newtonsoftSerializer.Deserialize(stringReader, type);
    }

    /// <summary>
    /// Deserializes the specified stream into an object of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize into.</typeparam>
    /// <param name="input">The stream containing the JSON to deserialize.</param>
    /// <returns>The deserialized object of type T.</returns>
    public T Deserialize<T>(Stream input)
    {
        using var streamReader = new StreamReader(input);
        return (T)_newtonsoftSerializer.Deserialize(streamReader, typeof(T));
    }
}