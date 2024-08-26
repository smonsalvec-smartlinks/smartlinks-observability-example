namespace CountriesProvider;

using System.Text.Json.Serialization;

internal class Maps
{
    [JsonPropertyName( "googleMaps" )]
    public string GoogleMaps { get; set; }

    [JsonPropertyName( "openStreetMaps" )]
    public string OpenStreetMaps { get; set; }
}
