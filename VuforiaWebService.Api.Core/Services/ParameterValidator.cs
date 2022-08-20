using System.Text.RegularExpressions;

namespace VuforiaWebService.Api.Core.Services;

/// <summary>Logic for validating a parameter.</summary>
public static class ParameterValidator
{
    /// <summary>Validates a parameter value against the methods regex.</summary>
    public static bool ValidateRegex(IParameter param, string paramValue)
    {
        return !string.IsNullOrEmpty(param.Pattern) ? new Regex(param.Pattern).IsMatch(paramValue) : true;
    }

    /// <summary>Validates if a parameter is valid.</summary>
    public static bool ValidateParameter(IParameter parameter, string value)
    {
        return string.IsNullOrEmpty(value) ? !parameter.IsRequired : ParameterValidator.ValidateRegex(parameter, value);
    }
}
