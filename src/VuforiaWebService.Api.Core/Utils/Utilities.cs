using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using VuforiaWebService.Api.Core.Extensions;

namespace VuforiaWebService.Api.Core.Utils;

public static class Utilities
{
    /// <summary>Returns the version of the core library.</summary>
    public static string GetLibraryVersion() => Regex.Match(typeof(Utilities).Assembly.FullName, "Version=([\\d\\.]+)").Groups[1].ToString();
    /// <summary>
    /// A VuforiaPortal.Apis utility method for returning the first matching custom attribute (or <c>null</c>) of the specified member.
    /// </summary>
    public static T GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
    {
        object[] customAttributes = info.GetCustomAttributes(typeof(T), false);
        return customAttributes.Length != 0 ? (T)customAttributes[0] : default;
    }

    /// <summary>Returns the defined string value of an Enum.</summary>
    internal static string GetStringValue(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        field.ThrowIfNull(nameof(value));
        StringValueAttribute customAttribute = field.GetCustomAttribute<StringValueAttribute>();
        return customAttribute != null
            ? customAttribute.Text
            : throw new ArgumentException(string.Format("Enum value '{0}' does not contain a StringValue attribute", field), nameof(value));
    }

    /// <summary>
    /// Returns the defined string value of an Enum. Use for test purposes or in other VuforiaPortal.Apis projects.
    /// </summary>
    public static string GetEnumStringValue(Enum value) => value.GetStringValue();

    /// <summary>
    /// Tries to convert the specified object to a string. Uses custom type converters if available.
    /// Returns null for a null object.
    /// </summary>
    public static string ConvertToString(object o)
    {
        if (o == null)
            return null;
        if (o.GetType().IsEnum)
        {
            StringValueAttribute customAttribute = o.GetType().GetField(o.ToString()).GetCustomAttribute<StringValueAttribute>();
            return customAttribute == null ? o.ToString() : customAttribute.Text;
        }
        return o is DateTime ? ConvertToRFC3339((DateTime)o) : o.ToString();
    }

    /// <summary>Converts the input date into a RFC3339 string (http://www.ietf.org/rfc/rfc3339.txt).</summary>
    internal static string ConvertToRFC3339(DateTime date)
    {
        if (date.Kind == DateTimeKind.Unspecified)
            date = date.ToUniversalTime();
        return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", DateTimeFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Parses the input string and returns <see cref="T:System.DateTime" /> if the input is a valid
    /// representation of a date. Otherwise it returns <c>null</c>.
    /// </summary>
    public static DateTime? GetDateTimeFromString(string raw) => !DateTime.TryParse(raw, out DateTime result) ? new DateTime?() : new DateTime?(result);

    /// <summary>Returns a string (by RFC3339) form the input <see cref="T:System.DateTime" /> instance.</summary>
    public static string GetStringFromDateTime(DateTime? date) => !date.HasValue ? null : ConvertToRFC3339(date.Value);
}