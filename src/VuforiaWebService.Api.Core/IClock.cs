namespace VuforiaWebService.Api.Core;

/// <summary>
/// Defines a clock interface for getting the current time.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets the current date and time as a <see cref="DateTime"/> object,
    /// expressed in the local time of the machine.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> object representing the current local date and time.
    /// </value>
    DateTime Now { get; }

    /// <summary>
    /// Gets the current date and time as a <see cref="DateTime"/> object,
    /// expressed in Coordinated Universal Time (UTC).
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> object representing the current UTC date and time.
    /// </value>
    DateTime UtcNow { get; }
}