using System;
using VuforiaWebService.Api.Core.Logger;

namespace VuforiaWebService.Api.Core;

public static class ApplicationContext
{
    private static ILogger logger;

    /// <summary>Returns the logger used within this application context.</summary>
    /// <remarks>It creates a <see cref="T:VuforiaPortal.Apis.Logging.NullLogger" /> if no logger was registered previously</remarks>
    public static ILogger Logger => logger ?? (logger = new NullLogger());

    /// <summary>Registers a logger with this application context.</summary>
    /// <exception cref="T:System.InvalidOperationException">Thrown if a logger was already registered.</exception>
    public static void RegisterLogger(ILogger loggerToRegister)
    {
        if (logger != null && !(logger is NullLogger))
            throw new InvalidOperationException("A logger was already registered with this context.");
        logger = loggerToRegister;
    }
}
