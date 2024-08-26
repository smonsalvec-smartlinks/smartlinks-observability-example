namespace APITest;

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class RequestTelemetryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TelemetryClient telemetryClient;
    private readonly List<string> methodsWithAllowedBody = [];

    public RequestTelemetryMiddleware( RequestDelegate next, TelemetryClient telemetryClient )
    {
        this._next = next;
        this.telemetryClient = telemetryClient;

        this.methodsWithAllowedBody.Add( HttpMethod.Post.ToString() );
        this.methodsWithAllowedBody.Add( HttpMethod.Put.ToString() );
        this.methodsWithAllowedBody.Add( HttpMethod.Patch.ToString() );
    }

    public async Task InvokeAsync( HttpContext context )
    {
        this.TrackHeaders( context );
        await this.TrackBodyMessage( context );

        await this._next( context );

        this.TrackResponse( context );
    }

    private void TrackHeaders( HttpContext context )
    {
        var trace = new TraceTelemetry( $"Headers" );

        foreach( var header in context.Request.Headers )
        {
            trace.Properties.Add( header.Key, header.Value.ToString() );
        }

        this.telemetryClient.TrackTrace( trace );
    }

    private async Task TrackBodyMessage( HttpContext context )
    {
        if( this.methodsWithAllowedBody.Contains( context.Request.Method ) )
        {
            context.Request.EnableBuffering();

            var body = string.Empty;
            using( var reader = new StreamReader( context.Request.Body, leaveOpen: true ) )
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var trace = new TraceTelemetry( $"Body: {body}" );
            this.telemetryClient.TrackTrace( trace );
        }
    }

    private async Task TrackResponse( HttpContext context )
    {
        if( context.Response.StatusCode == StatusCodes.Status200OK && context.Response.Body.Length > 0 )
        {
            HttpResponse response = context.Response;
            var originalResponseBody = response.Body;
            using var newResponseBody = new MemoryStream();
            response.Body = newResponseBody;

            newResponseBody.Seek( 0, SeekOrigin.Begin );
            var responseBodyText =
                await new StreamReader( response.Body ).ReadToEndAsync();

            newResponseBody.Seek( 0, SeekOrigin.Begin );
            await newResponseBody.CopyToAsync( originalResponseBody );
    
            var trace = new TraceTelemetry( $"Response: {responseBodyText}" );
        }
    }
}
