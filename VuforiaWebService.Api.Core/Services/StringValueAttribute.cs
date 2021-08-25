using System;

namespace VuforiaWebService.Api.Core.Services
{
    /// <summary>Defines an attribute containing a string representation of the member.</summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringValueAttribute : Attribute
    {
        private readonly string text;

        /// <summary>The text which belongs to this member.</summary>
        public string Text
        {
            get
            {
                return this.text;
            }
        }

        /// <summary>Creates a new string value attribute with the specified text.</summary>
        public StringValueAttribute(string text)
        {
            text.ThrowIfNull<string>(nameof(text));
            this.text = text;
        }
    }
}
