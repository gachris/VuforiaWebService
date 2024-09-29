using System;
using VuforiaWebService.Api.Core.Extensions;

namespace VuforiaWebService.Api.Core;

/// <summary>Defines an attribute containing a string representation of the member.</summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class StringValueAttribute : Attribute
{
    private readonly string text;

    /// <summary>The text which belongs to this member.</summary>
    public string Text => text;

    /// <summary>Creates a new string value attribute with the specified text.</summary>
    public StringValueAttribute(string text)
    {
        text.ThrowIfNull(nameof(text));
        this.text = text;
    }
}
