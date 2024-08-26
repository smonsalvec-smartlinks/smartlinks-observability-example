namespace Infrastructure;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

public class SessionTelemetryInitializer( IHttpContextAccessor httpContextAccessor ) : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public void Initialize( ITelemetry telemetry )
    {
        var context = this._httpContextAccessor.HttpContext;

        if( context == null )
            return;

        string authHeader = context.Request.Headers["Authorization"];

        if( string.IsNullOrEmpty( authHeader ) )
            return;

        string token = authHeader.Split( ' ' ).Last() ?? string.Empty;

        if( string.IsNullOrEmpty( token ) )
            return;

        string body = token.Split( '.' )[1];
        byte[] decodedBytes = Convert.FromBase64String( body );
        string decodedBody = Encoding.UTF8.GetString( decodedBytes );

        using JsonDocument doc = JsonDocument.Parse( decodedBody );
        JsonElement root = doc.RootElement;

        if( root.TryGetProperty( "userId", out JsonElement userIdElement ) )
        {
            telemetry.Context.User.UserAgent = context.Request.Headers["User-Agent"];
            telemetry.Context.Session.Id = root.GetProperty( "jti" ).GetString() ?? string.Empty;
            telemetry.Context.User.Id = userIdElement.GetString() ?? string.Empty;
            telemetry.Context.User.AuthenticatedUserId = root.GetProperty( "email" ).GetString() ?? string.Empty;
            telemetry.Context.User.AccountId = root.GetProperty( "name" ).GetString() ?? string.Empty;
            telemetry.Context.Properties.Add( "Agency Id", root.GetProperty( "agencyId" ).GetString() ?? string.Empty );
            telemetry.Context.Properties.Add( "Agency Name", root.GetProperty( "agencyName" ).GetString() ?? string.Empty );
        }
    }
}
