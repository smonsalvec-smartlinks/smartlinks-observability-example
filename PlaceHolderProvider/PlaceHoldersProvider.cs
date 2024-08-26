namespace PlaceHolderProvider;

using Infrastructure;
using Observability.Domain;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class PlaceHoldersProvider : IPlaceHolderProvider
{
    private readonly HttpClient _httpClient;
    private readonly string baseUrl = "https://jsonplaceholder.typicode.com/";

    public PlaceHoldersProvider( HttpClient httpClient )
    {
        this._httpClient = httpClient;
    }

    public async Task<Observability.Domain.PlaceHolder> CreateAsync( Observability.Domain.PlaceHolder placeHolder )
    {
        try
        {
            string json = JsonSerializer.Serialize( placeHolder );
            HttpContent httpContent = new StringContent( json, Encoding.UTF8, "application/json" );
            HttpResponseMessage response = await this._httpClient.PostAsync( $"{this.baseUrl}/posts", httpContent );

            if( response.IsSuccessStatusCode )
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ContainerServices.TelemetryClient.TrackTrace( $"Response: {responseContent ?? string.Empty}" );
                PlaceHolder placeHolders = JsonSerializer.Deserialize<PlaceHolder>( responseContent );

                return  new Observability.Domain.PlaceHolder
                (
                    placeHolder.Id,
                    placeHolder.UserId,
                    placeHolder.Title,
                    placeHolder.Body
                );
            }
            else
            {
                ContainerServices.TelemetryClient.TrackException( new Exception( $"Failed to get placeholders. Status code: {response.StatusCode}" ) );
                throw new Exception( $"Failed to get countries. Status code: {response.StatusCode}" );
            }
        }
        catch( Exception ex )
        {
            ContainerServices.TelemetryClient.TrackException( ex );
            throw ex;
        }
    }

    public async Task<IEnumerable<Observability.Domain.PlaceHolder>> GetAllAsync()
    {
        try
        {
            HttpResponseMessage response = await this._httpClient.GetAsync( $"{this.baseUrl}/posts" );

            if( response.IsSuccessStatusCode )
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ContainerServices.TelemetryClient.TrackTrace( $"Response: {responseContent ?? string.Empty}" );
                List<PlaceHolder> placeHolders = JsonSerializer.Deserialize<List<PlaceHolder>>( responseContent );

                return placeHolders.Select( placeHolder => new Observability.Domain.PlaceHolder
                (
                    placeHolder.Id,
                    placeHolder.UserId,
                    placeHolder.Title,
                    placeHolder.Body
                ) );
            }
            else
            {
                ContainerServices.TelemetryClient.TrackException( new Exception( $"Failed to get placeholders. Status code: {response.StatusCode}" ) );
                throw new Exception( $"Failed to get countries. Status code: {response.StatusCode}" );
            }
        }
        catch( Exception ex )
        {
            ContainerServices.TelemetryClient.TrackException( ex );
            throw ex;
        }
    }
}
