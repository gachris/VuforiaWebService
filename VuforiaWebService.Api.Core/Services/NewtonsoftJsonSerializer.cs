using Newtonsoft.Json;
using System;
using System.IO;

namespace VuforiaWebService.Api.Core.Services;

/// <summary>Class for serialization and deserialization of JSON documents using the Newtonsoft Library.</summary>
public class NewtonsoftJsonSerializer : IJsonSerializer, ISerializer
{
    private static readonly JsonSerializer newtonsoftSerializer;
    private static NewtonsoftJsonSerializer instance;

    /// <summary>A singleton instance of the Newtonsoft JSON Serializer.</summary>
    public static NewtonsoftJsonSerializer Instance
    {
        get
        {
            return NewtonsoftJsonSerializer.instance = NewtonsoftJsonSerializer.instance ?? new NewtonsoftJsonSerializer();
        }
    }

    static NewtonsoftJsonSerializer()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.Converters.Add(new RFC3339DateTimeConverter());
        NewtonsoftJsonSerializer.newtonsoftSerializer = JsonSerializer.Create(settings);
    }

    public string Format
    {
        get
        {
            return "json";
        }
    }

    public void Serialize(object obj, Stream target)
    {
        using StreamWriter streamWriter = new StreamWriter(target);
        if (obj == null)
            obj = (object)string.Empty;
        NewtonsoftJsonSerializer.newtonsoftSerializer.Serialize((TextWriter)streamWriter, obj);
    }

    public string Serialize(object obj)
    {
        using TextWriter textWriter = (TextWriter)new StringWriter();
        if (obj == null)
            obj = (object)string.Empty;
        NewtonsoftJsonSerializer.newtonsoftSerializer.Serialize(textWriter, obj);
        return textWriter.ToString();
    }

    public T Deserialize<T>(string input)
    {
        return string.IsNullOrEmpty(input) ? default : JsonConvert.DeserializeObject<T>(input);
    }

    public object Deserialize(string input, Type type)
    {
        return string.IsNullOrEmpty(input) ? (object)null : JsonConvert.DeserializeObject(input, type);
    }

    public T Deserialize<T>(Stream input)
    {
        using StreamReader streamReader = new StreamReader(input);
        return (T)NewtonsoftJsonSerializer.newtonsoftSerializer.Deserialize((TextReader)streamReader, typeof(T));
    }
}
