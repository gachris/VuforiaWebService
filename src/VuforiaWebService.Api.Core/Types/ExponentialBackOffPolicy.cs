using System;

namespace VuforiaWebService.Api.Core.Types;

[Flags]
public enum ExponentialBackOffPolicy
{
    /// <summary>Exponential back-off is disabled.</summary>
    None = 0,
    /// <summary>Exponential back-off is enabled only for exceptions.</summary>
    Exception = 1,
    /// <summary>Exponential back-off is enabled only for 503 HTTP Status code.</summary>
    UnsuccessfulResponse503 = 2,
}