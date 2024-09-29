/* Unmerged change from project 'VuforiaWebService.Api.Core (netstandard2.0)'
Before:
using System;
using System.IO;
After:
using System.IO;
using Newtonsoft.Json;
*/

namespace VuforiaWebService.Api.Core.Serialization;

/// <summary>Class for serialization and deserialization of JSON documents using the Newtonsoft Library.</summary>
public class NewtonsoftJsonSerializer : IJsonSerializer, ISerializer
{
    private static readonly JsonSerializer newtonsoftSerializer;
    private static NewtonsoftJsonSerializer instance;

    /// <summary>A singleton instance of the Newtonsoft JSON Serializer.</summary>
    public static NewtonsoftJsonSerializer Instance => instance = instance ?? new NewtonsoftJsonSerializer();

    static NewtonsoftJsonSerializer()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.Converters.Add(new RFC3339DateTimeConverter());
        newtonsoftSerializer = JsonSerializer.Create(settings);
    }
    /// <inheritdoc/>

    public string Format => "json";
    /// <inheritdoc/>

    public void Serialize(object obj, Stream target)
    {
        using StreamWriter streamWriter = new StreamWriter(target);
        if (obj == null)
            obj = string.Empty;
        newtonsoftSerializer.Serialize(streamWriter, obj);
    }
    /// <inheritdoc/>

    public string Serialize(object obj)
    {
        using TextWriter textWriter = new StringWriter();
        if (obj == null)
            obj = string.Empty;
        newtonsoftSerializer.Serialize(textWriter, obj);
        return textWriter.ToString();
    }
    /// <inheritdoc/>

    public T Deserialize<T>(string input) => string.IsNullOrEmpty(input) ? default : JsonConvert.DeserializeObject<T>(input);
    /// <inheritdoc/>

    public object Deserialize(string input, Type type) => string.IsNullOrEmpty(input) ? null : JsonConvert.DeserializeObject(input, type);
    /// <inheritdoc/>

    public T Deserialize<T>(Stream input)
    {
        using StreamReader streamReader = new StreamReader(input);
        return (T)newtonsoftSerializer.Deserialize(streamReader, typeof(T));
    }
}
