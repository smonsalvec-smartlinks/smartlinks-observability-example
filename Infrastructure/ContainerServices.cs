namespace Infrastructure;

using Microsoft.ApplicationInsights;

public static class ContainerServices
{
    public static TelemetryClient TelemetryClient { get; set; }
}
