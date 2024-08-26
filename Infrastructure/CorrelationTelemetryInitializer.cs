namespace Infrastructure;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

public class CorrelationTelemetryInitializer( IHttpContextAccessor httpContextAccessor ) : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public void Initialize( ITelemetry telemetry )
    {
        var context = this._httpContextAccessor.HttpContext;

        if( context == null )
            return;

        if( telemetry is RequestTelemetry || telemetry is ExceptionTelemetry || telemetry is DependencyTelemetry )
            return;

        // Obtener el RequestTelemetry del contexto actual
        var requestTelemetry = context.Features.Get<RequestTelemetry>();

        if( requestTelemetry == null )
            return;

        telemetry.Context.Operation.Id = requestTelemetry.Context.Operation.Id;
        telemetry.Context.Operation.ParentId = requestTelemetry.Id;
    }
}