using System;

namespace VuforiaWebService.Api.Core.Util;

/// <summary>A default clock implementation that wraps the <see cref="P:System.DateTime.Now" /> property.</summary>
public class SystemClock : IClock
{
    /// <summary>The default instance.</summary>
    public static readonly IClock Default = new SystemClock();

    /// <summary>Constructs a new system clock.</summary>
    protected SystemClock()
    {
    }

    public DateTime Now
    {
        get
        {
            return DateTime.Now;
        }
    }

    public DateTime UtcNow
    {
        get
        {
            return DateTime.UtcNow;
        }
    }
}
