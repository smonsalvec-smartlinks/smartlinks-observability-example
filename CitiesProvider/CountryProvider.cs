namespace CountriesProvider;

using Infrastructure;
using Observability.Domain;
using System.Text.Json;
using System.Threading.Tasks;

public class CountryProvider : ICountryProvider
{
    private readonly HttpClient _httpClient;
    private readonly string baseUrl = "https://restcountries.com/v3.1/";

    public CountryProvider( HttpClient httpClient )
    {
        this._httpClient = httpClient;
    }

    public async Task<IEnumerable<Observability.Domain.Country>> GetCountryByNameAsync( string name )
    {
        try
        {
            HttpResponseMessage response = await this._httpClient.GetAsync( $"{this.baseUrl}name/{name.Trim()}" );

            if( response.IsSuccessStatusCode )
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ContainerServices.TelemetryClient.TrackTrace( $"Response: {responseContent ?? string.Empty}" );
                List<Country> countries = JsonSerializer.Deserialize<List<Country>>( responseContent );

                return countries.Select( country => new Observability.Domain.Country
                (
                    country.Name.Common,
                    country.Capital.FirstOrDefault() ?? string.Empty,
                    country.Latlng ?? [],
                    country.Area,
                    country.Population,
                    country.StartOfWeek
                ) );
            }
            else
            {
                throw new Exception( $"Failed to get countries for name {name}. Status code: {response.StatusCode}" );
            }
        }
        catch( Exception ex )
        {
            throw ex;
        }
    }
}
