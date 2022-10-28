namespace VuforiaWebService.Api.Core;

/// <summary>A single server error</summary>
public class SingleError
{
    /// <summary>The domain in which the error occured</summary>
    public string Domain { get; set; }

    /// <summary>The reason the error was thrown</summary>
    public string Reason { get; set; }

    /// <summary>The error message</summary>
    public string Message { get; set; }

    /// <summary>Type of the location</summary>
    public string LocationType { get; set; }

    /// <summary>Location where the error was thrown</summary>
    public string Location { get; set; }

    public override string ToString() => string.Format("Message[{0}] Location[{1} - {2}] Reason[{3}] Domain[{4}]", Message, Location, LocationType, Reason, Domain);
}
