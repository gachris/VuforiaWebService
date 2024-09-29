﻿namespace VuforiaWebService.Api.Core;

/// <summary>Strategy interface to control back-off between retry attempts.</summary>
public interface IBackOff
{
    /// <summary>
    /// Gets the a time span to wait before next retry. If the current retry reached the maximum number of retries,
    /// the returned value is <see cref="F:System.TimeSpan.MinValue" />.
    /// </summary>
    TimeSpan GetNextBackOff(int currentRetry);

    /// <summary>Gets the maximum number of retries.</summary>
    int MaxNumOfRetries { get; }
}
