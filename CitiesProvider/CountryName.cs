namespace CountriesProvider;

using System.Text.Json.Serialization;

internal class CountryName
{
    [JsonPropertyName( "common" )]
    public string Common { get; set; }
    [JsonPropertyName( "official" )]
    public string Official { get; set; }
}
